using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickGenerator : MonoBehaviour
{
    // ��������M�~�b�N�̃v���n�u
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
    /// �M�~�b�N�𐶐�����
    /// </summary>
    /// <param name="parent"></param>
    public void GenerateGimmick(GameObject parent)
    {
        // �X�e�[�W�ҏW���[�h�łȂ��ꍇ�M�~�b�N�𐶐����Ĕz�u�ł���悤�ɂ���
        if(!StageEditor.editStageFlg) Instantiate(generateObj, parent.transform);
    }
}
