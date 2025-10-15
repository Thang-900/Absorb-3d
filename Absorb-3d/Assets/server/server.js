// server.js

const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();

// Middleware
app.use(bodyParser.json());
app.use(cors());

// --- KẾT NỐI MONGODB ATLAS ---
const MONGODB_URI = "mongodb+srv://Thang:Independent123@playerdata.ltqr5hy.mongodb.net/game_db?retryWrites=true&w=majority&appName=PlayerData";

mongoose.connect(MONGODB_URI)
    .then(() => console.log("✅ MongoDB Atlas connected"))
    .catch(err => console.error("❌ MongoDB connection error:", err));

// --- ĐỊNH NGHĨA SCHEMA PLAYER ---
const playerSchema = new mongoose.Schema({
    playerId: { type: String, required: true, unique: true },
    gold: { type: Number, default: 0 },
    diamond: { type: Number, default: 0 },
    level: { type: Number, default: 1 }
});

const Player = mongoose.model('Player', playerSchema);

// --- ROUTE LẤY THÔNG TIN PLAYER ---
app.get('/player/:id', async (req, res) => {
    try {
        const player = await Player.findOne({ playerId: req.params.id });
        if (player) res.json(player);
        else res.status(404).json({ message: "Player not found" });
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

// --- ROUTE TẠO HOẶC CẬP NHẬT PLAYER ---
app.post('/player', async (req, res) => {
    const { playerId, gold, diamond, level } = req.body;

    if (!playerId) {
        return res.status(400).json({ message: "playerId is required" });
    }

    try {
        let player = await Player.findOne({ playerId });
        if (!player) {
            // Tạo mới
            player = new Player({ playerId, gold, diamond, level });
        } else {
            // Cập nhật
            player.gold = gold ?? player.gold;
            player.diamond = diamond ?? player.diamond;
            player.level = level ?? player.level;
        }
        await player.save();
        res.json(player);
    } catch (err) {
        res.status(500).json({ message: err.message });
    }
});

// --- CHẠY SERVER ---
const PORT = 3000;
app.listen(PORT, () => {
    console.log(`🚀 Server running on port ${PORT}`);
});
