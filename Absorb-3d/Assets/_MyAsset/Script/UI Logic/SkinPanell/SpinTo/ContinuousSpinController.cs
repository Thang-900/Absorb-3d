using UnityEngine;
using UnityEngine.UI;

public class ContinuousSpinController : MonoBehaviour
{
    [Header("References")]
    public RectTransform content;

    [Header("Spin Settings")]
    public float startSpeed = 800f;      // tốc độ khi bắt đầu quay
    public float slowDistance = 300f;    // khoảng cách bắt đầu giảm tốc
    public float slowFactor = 3f;        // hệ số giảm tốc
    public float minStopSpeed = 10f;     // tốc độ ngưỡng để snap dừng

    private float speed;
    private float targetPos;
    public string nameOfSkin;
    private bool spinning = false;
    private bool slowing = false;

    [Header("Spawn object")]
    public GameObject[] spawTabSkin;
    public GameObject awardPanel;
    void Update()
    {
        if (!spinning) return;

        // ► 3. Di chuyển content sang trái
        content.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        // ► 4. Khi gần đến vị trí dừng → kích hoạt giảm tốc
        if (!slowing && Mathf.Abs(content.anchoredPosition.x - targetPos) < slowDistance)
            slowing = true;

        if (slowing)
        {
            // Giảm tốc mượt mà bằng Lerp
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * slowFactor);

            // ► 5. Khi tốc độ rất nhỏ → dừng hẳn
            if (speed < minStopSpeed)
            {
                // Snap đúng vị trí item đã random
                content.anchoredPosition = new Vector2(targetPos, content.anchoredPosition.y);

                spinning = false;
                slowing = false;

                DataManager.currentData.ListSkinOwned.Add(nameOfSkin);
                DataManager.SaveAll();
                Debug.Log("✔ Đã thêm skin: " + nameOfSkin);
                ShowNewSkin();
            }
        }
    }
    private void ShowNewSkin()
    {
        foreach(GameObject tab in spawTabSkin)
        {
            if(tab.name==nameOfSkin)
            {
                awardPanel.SetActive(true);
                var obj= Instantiate(tab,awardPanel.transform);
                obj.transform.localScale *= 1.1f;
                obj.transform.localPosition=Vector3.zero;
            }
        }
    }
    // ----------------------------------------------------------------------------------------
    // ► 2. Random chọn item và tính targetPosition
    // ----------------------------------------------------------------------------------------
    public void StartSpin()
    {
        if (content.childCount == 0) return;

        int index;

        // Random cho đến khi KHÔNG trùng
        do
        {
            index = Random.Range(4, content.childCount - 5);
            nameOfSkin=content.GetChild(index).name.ToString();
        }
        while (DataManager.currentData.ListSkinOwned.Contains(nameOfSkin));
        // Bước 1 — chọn item ngẫu nhiên
        RectTransform item = content.GetChild(index).GetComponent<RectTransform>();
        // Bước 2 — lấy vị trí thật của item → tính điểm dừng
        targetPos = -item.anchoredPosition.x;
        //luu lai skin da trung thuong
        // Reset trạng thái
        speed = startSpeed;
        spinning = true;
        slowing = false;

        Debug.Log("🎯 Target Item Index = " + index);
    }
}
