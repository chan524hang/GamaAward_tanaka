using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEditor : MonoBehaviour
{
    // �X�e�[�W�ҏW�ۃt���O
    public static bool editStageFlg;
    private GameObject editObj;
    // �}�E�X�ŃN���b�N�������W
    private Vector3 clickPos;
    // �X�e�[�W���̃M�~�b�N
    private List<GameObject> gimmicks = new List<GameObject>();

    private float maxPosX, minPosX, maxPosY, minPosY;
    // �I�u�W�F�N�g����N���b�N�ۂ͈̔�
    private const float POS_RANGE = 0.5f;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // �S�Ă�GameObject�^��z��Ŏ擾���A���̗v�f�����J��Ԃ��M�~�b�N����������
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag != "Untagged" && obj.tag != "MainCamera")
            {
                gimmicks.Add(obj);
                Debug.Log(obj.name);
            }
        }

        // �X�e�[�W��ҏW�ł���悤�ɂ���
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            editStageFlg = !editStageFlg;
            if (editStageFlg) Debug.Log("�X�e�[�W�ҏW���[�h");
        }

        if (editStageFlg)
        {
            //  �N���b�N������ʂ̍��W���擾����
            if (Input.GetMouseButtonDown(0)) clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            foreach (GameObject obj in gimmicks)
            {
                CalcuratePositionRange(obj);
                if (clickPos.x > minPosX && clickPos.x < maxPosX && clickPos.y > minPosY && clickPos.y < maxPosY)
                {
                    editObj = obj;
                    //Debug.Log(editObj.name + "���I������܂���");
                }
            }

            if (editObj != null) MoveGimmik(editObj);
        }
    }


    /// <summary>
    /// �M�~�b�N�̃N���b�N�۔͈͂��v�Z����
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
