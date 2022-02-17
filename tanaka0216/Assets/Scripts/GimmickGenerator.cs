using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    // 生成するギミックのプレハブ
    [SerializeField]
    private GameObject generateObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ギミックを生成する
    /// </summary>
    /// <param name="parent"></param>
    public void GenerateGimmick(GameObject parent)
    {
        // ステージ編集モードでない場合ギミックを生成して配置できるようにする
        if(!StageEditor.editStageFlg) Instantiate(generateObj, parent.transform);
    }
}
