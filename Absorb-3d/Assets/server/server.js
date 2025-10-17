// --- IMPORT THƯ VIỆN CẦN THIẾT ---
const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
app.use(bodyParser.json());
app.use(cors());

// --- KẾT NỐI MONGODB ATLAS ---
const MONGODB_URI = "mongodb+srv://Thang:Independent@playerdata.ltqr5hy.mongodb.net/unitycrud?retryWrites=true&w=majority&appName=PlayerData";
mongoose.connect(MONGODB_URI)
    .then(() => console.log("✅ MongoDB Atlas connected"))
    .catch(err => console.error("❌ MongoDB connection error:", err));

// --- SCHEMA CHO PLAYER ---
const playerSchema = new mongoose.Schema({
    playerId: { type: String, required: true, unique: true },
    gold: { type: Number, default: 0 },
    diamond: { type: Number, default: 0 },
    levelMap: { type: Number, default: 1 },
});

// --- TẠO INDEX ĐỂ TỐI ƯU TÌM KIẾM ---
playerSchema.index({ playerId: 1 });   // Tạo chỉ mục (index) trên playerId
playerSchema.index({ levelMap: 1 });   // Tạo chỉ mục trên levelMap để tìm nhanh theo level

const Player = mongoose.model('Player', playerSchema);



// ==========================================================
// 🧩 1️⃣  ROUTE: AGGREGATION PIPELINE (THỐNG KÊ)
// ==========================================================
// Ví dụ: Tính tổng gold và diamond của từng levelMap
app.get('/stats', async (req, res) => {
    try {
        const result = await Player.aggregate([
            {
                $group: {
                    _id: "$levelMap",
                    totalGold: { $sum: "$gold" },
                    totalDiamond: { $sum: "$diamond" },
                    avgGold: { $avg: "$gold" }
                }
            },
            { $sort: { totalGold: -1 } }
        ]);
        res.json(result);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});



// ==========================================================
// 💾 2️⃣  ROUTE: TRANSACTION (GIAO DỊCH AN TOÀN)
// ==========================================================
// Ví dụ: Chuyển gold giữa 2 người chơi (atomic operation)
app.post('/transferGold', async (req, res) => {
    const { fromPlayerId, toPlayerId, amount } = req.body;

    if (!fromPlayerId || !toPlayerId || !amount)
        return res.status(400).json({ message: "Thiếu thông tin" });

    const session = await mongoose.startSession();
    session.startTransaction();

    try {
        const sender = await Player.findOne({ playerId: fromPlayerId }).session(session);
        const receiver = await Player.findOne({ playerId: toPlayerId }).session(session);

        if (!sender || !receiver)
            throw new Error("Không tìm thấy người chơi");

        if (sender.gold < amount)
            throw new Error("Không đủ vàng để chuyển");

        sender.gold -= amount;
        receiver.gold += amount;

        await sender.save({ session });
        await receiver.save({ session });

        await session.commitTransaction();
        session.endSession();

        res.json({ message: "Giao dịch thành công" });
    } catch (err) {
        await session.abortTransaction();
        session.endSession();
        res.status(500).json({ message: err.message });
    }
});



// ==========================================================
// 🧱 3️⃣  CRUD CƠ BẢN NHƯ CŨ
// ==========================================================

// --- LẤY PLAYER THEO ID ---
app.get('/player/:id', async (req, res) => {
    try {
        const player = await Player.findOne({ playerId: req.params.id });
        if (player) res.json(player);
        else res.status(404).json({ message: "Player not found" });
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});
// --- LẤY TẤT CẢ NGƯỜI CHƠI (GET /players) ---
app.get('/players', async (req, res) => {
    try {
        const players = await Player.find();
        res.json(players);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});


// --- TẠO HOẶC CẬP NHẬT PLAYER ---
app.post('/player', async (req, res) => {
    const { playerId, gold, diamond, levelMap } = req.body;

    if (!playerId) return res.status(400).json({ message: "playerId is required" });

    try {
        let player = await Player.findOne({ playerId });
        if (!player) {
            player = new Player({ playerId, gold, diamond, levelMap });
        } else {
            player.gold = gold ?? player.gold;
            player.diamond = diamond ?? player.diamond;
            player.levelMap = levelMap ?? player.levelMap;
        }
        await player.save();
        res.json(player);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

// --- TẠO COLLECTION ---
app.post('/createCollection/:name', async (req, res) => {
    const collectionName = req.params.name;
    try {
        const result = await mongoose.connection.db.collection(collectionName).insertOne({ createdAt: new Date() });
        res.json({ message: `Collection ${collectionName} created`, result });
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

// --- XÓA PLAYER THEO ID ---
app.delete('/player/:id', async (req, res) => {
    const playerId = req.params.id;

    try {
        const result = await Player.deleteOne({ playerId: playerId });
        if (result.deletedCount === 0) {
            res.status(404).json({ message: "Player not found" });
        } else {
            res.json({ message: `Player ${playerId} deleted successfully` });
        }
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});



// --- XÓA COLLECTION ---
app.delete('/deleteCollection/:name', async (req, res) => {
    const collectionName = req.params.name;
    try {
        await mongoose.connection.db.dropCollection(collectionName);
        res.json({ message: `Collection ${collectionName} deleted` });
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});
// 🪙 Cập nhật vàng người chơi (PUT /player/:id)
app.put("/player/:id", async (req, res) => {
    try {
        const playerId = req.params.id;

        // Tăng thêm 1000 vàng và 1 level
        const updatedPlayer = await Player.findOneAndUpdate(
            { playerId },
            { $inc: { gold: 1000, levelMap: 1 } },
            { new: true }
        );

        if (!updatedPlayer)
            return res.status(404).json({ error: "Không tìm thấy người chơi" });

        res.json({
            message: "✅ Cập nhật thành công!",
            updatedPlayer
        });
    } catch (err) {
        console.error(err);
        res.status(500).json({ error: "❌ Lỗi khi cập nhật dữ liệu" });
    }
});


// --- CHẠY SERVER ---
const PORT = 3000;
app.listen(PORT, () => {
    console.log(`🚀 Server running on port ${PORT}`);
});
