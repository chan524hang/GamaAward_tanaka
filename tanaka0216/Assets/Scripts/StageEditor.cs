using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEditor : MonoBehaviour
{
    // ステージ編集可否フラグ
    public static bool editStageFlg;
    private GameObject editObj;
    // マウスでクリックした座標
    private Vector3 clickPos;
    // ステージ内のギミック
    private List<GameObject> gimmicks = new List<GameObject>();

    private float maxPosX, minPosX, maxPosY, minPosY;
    // オブジェクトからクリック可否の範囲
    private const float POS_RANGE = 0.5f;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // 全てのGameObject型を配列で取得し、その要素数分繰り返しギミックを検索する
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag != "Untagged" && obj.tag != "MainCamera")
            {
                gimmicks.Add(obj);
                Debug.Log(obj.name);
            }
        }

        // ステージを編集できるようにする
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            editStageFlg = !editStageFlg;
            if (editStageFlg) Debug.Log("ステージ編集モード");
        }

        if (editStageFlg)
        {
            //  クリックした画面の座標を取得する
            if (Input.GetMouseButtonDown(0)) clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            foreach (GameObject obj in gimmicks)
            {
                CalcuratePositionRange(obj);
                if (clickPos.x > minPosX && clickPos.x < maxPosX && clickPos.y > minPosY && clickPos.y < maxPosY)
                {
                    editObj = obj;
                    //Debug.Log(editObj.name + "が選択されました");
                }
            }

            if (editObj != null) MoveGimmik(editObj);
        }
    }


    /// <summary>
    /// ギミックのクリック可否範囲を計算する
    /// </summary>
    /// <param name="obj"></param>
    private void CalcuratePositionRange(GameObject obj)
    {
        maxPosX = obj.transform.position.x + POS_RANGE;
        minPosX = obj.transform.position.x - POS_RANGE;

        maxPosY = obj.transform.position.y + POS_RANGE;
        minPosY = obj.transform.position.y - POS_RANGE;
    }

    private void MoveGimmik(GameObject obj)
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 movePos = Input.mousePosition;
            movePos.z = -10f;
            obj.transform.position = Camera.main.ScreenToWorldPoint(movePos);
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0f);
        }
    }
}
