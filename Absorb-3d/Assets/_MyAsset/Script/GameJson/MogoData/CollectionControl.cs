using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CollectionControl : MonoBehaviour
{
    [Header("Tên collection muốn tạo/xóa")]
    public string collectionName;

    public void OnCreateButton()
    {
        StartCoroutine(CreateCollection());
    }

    public void OnDeleteButton()
    {
        StartCoroutine(DeleteCollection());
    }

    IEnumerator CreateCollection()
    {
        string url = "http://localhost:3000/createCollection/" + collectionName;

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(new byte[0]);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log("Created: " + www.downloadHandler.text);
            else
                Debug.Log("Error: " + www.error);
        }
    }

    IEnumerator DeleteCollection()
    {
        string url = "http://localhost:3000/deleteCollection/" + collectionName;

        using (UnityWebRequest www = new UnityWebRequest(url, "DELETE"))
        {
            www.uploadHandler = new UploadHandlerRaw(new byte[0]);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log("Deleted: " + www.downloadHandler.text);
            else
                Debug.Log("Error: " + www.error);
        }
    }
}
