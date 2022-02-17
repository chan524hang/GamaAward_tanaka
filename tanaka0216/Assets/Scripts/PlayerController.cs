using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* ステージ編集でオブジェクトを動かせるようにするよ
       オブジェクトの性質を変更できるようにするよ */

    /// <summary>
    /// ふつうの反射フラグ
    /// </summary>
    public static bool normalReflectFlg;
    /// <summary>
    /// 反発する反射フラグ
    /// </summary>
    public static bool reboundReflectFlg;
    /// <summary>
    /// 吸収する反射フラグ
    /// </summary>
    public static bool absorbReflectFlg;

    // ステージ編集可否フラグ
    private bool editStageFlg;
    private GameObject editObj;
    // マウスでクリックした座標
    private Vector3 clickPos;
    // ステージ内のギミック
    private List<GameObject> gimmicks = new List<GameObject>();

    private float maxPosX, minPosX, maxPosY, minPosY;
    // オブジェクトからクリック可否の範囲
    private const float POS_RANGE = 0.5f;

    [SerializeField]
    private GameObject debugPoint;


    // Start is called before the first frame update
    void Start()
    {
        // 全てのGameObject型を配列で取得し、その要素数分繰り返しギミックを検索する
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag != "Untagged" && obj.tag != "MainCamera")
            {
                gimmicks.Add(obj);
                //Debug.Log(obj.name);
            }
        }

        // デバッグ用範囲ポイントを生成する
        foreach (GameObject obj in gimmicks)
        {
            CalcuratePositionRange(obj);
            GenerateDebugPoint(maxPosX, minPosX, maxPosY, minPosY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ステージを編集できるようにする
        if(Input.GetKeyDown(KeyCode.RightAlt))
        {
            editStageFlg = !editStageFlg;
            if(editStageFlg) Debug.Log("ステージ編集モード");
        }

        if(editStageFlg)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //  クリックした画面の座標を取得する
                clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            foreach (GameObject obj in gimmicks)
            {
                CalcuratePositionRange(obj);
                if (clickPos.x > minPosX && clickPos.x < maxPosX && clickPos.y > minPosY && clickPos.y < maxPosY)
                {
                    editObj = obj;
                    //Debug.Log(editObj.name + "の範囲内です");
                }
            }

            if (editObj != null)
            {
                // ギミックの属性を変更する
            }
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

    /// <summary>
    /// ギミックのクリック可否範囲を表示する
    /// </summary>
    /// <param name="maxX"></param>
    /// <param name="minX"></param>
    /// <param name="maxY"></param>
    /// <param name="minY"></param>
    private void GenerateDebugPoint(float maxX,float minX,float maxY,float minY)
    {
        Instantiate(debugPoint, new Vector3(maxX, maxY, debugPoint.transform.position.z), Quaternion.identity);
        Instantiate(debugPoint, new Vector3(maxX, minY, debugPoint.transform.position.z), Quaternion.identity);
        Instantiate(debugPoint, new Vector3(minX, maxY, debugPoint.transform.position.z), Quaternion.identity);
        Instantiate(debugPoint, new Vector3(minX, minY, debugPoint.transform.position.z), Quaternion.identity);
    }

    private void ChangeGimmickType()
    {

    }

    private void MoveGimmik(GameObject obj)
    {
        if (Input.GetMouseButton(0))
        {
            //  クリックした画面の座標を取得する
            Vector3 movePos = Input.mousePosition;
            movePos.z = -10f;
            obj.transform.position = Camera.main.ScreenToWorldPoint(movePos);
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string collisionTag = collision.gameObject.tag;

        switch(collisionTag)
        {
            // 普通の衝突
            case "Nail":
                normalReflectFlg = true;
                //Debug.Log("釘に衝突しました");
                break;
            // 反発する反射
            case "Balloon":
                reboundReflectFlg = true;
                //Debug.Log("風船に衝突しました");
                break;
            // 吸収する反射
            case "Pillow":
                absorbReflectFlg = true;
                //Debug.Log("枕に衝突しました");
                break;
        }
    }
}
