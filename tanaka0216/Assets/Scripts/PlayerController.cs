using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* �X�e�[�W�ҏW�ŃI�u�W�F�N�g�𓮂�����悤�ɂ����
       �I�u�W�F�N�g�̐�����ύX�ł���悤�ɂ���� */

    /// <summary>
    /// �ӂ��̔��˃t���O
    /// </summary>
    public static bool normalReflectFlg;
    /// <summary>
    /// �������锽�˃t���O
    /// </summary>
    public static bool reboundReflectFlg;
    /// <summary>
    /// �z�����锽�˃t���O
    /// </summary>
    public static bool absorbReflectFlg;

    // �X�e�[�W�ҏW�ۃt���O
    private bool editStageFlg;
    private GameObject editObj;
    // �}�E�X�ŃN���b�N�������W
    private Vector3 clickPos;
    // �X�e�[�W���̃M�~�b�N
    private List<GameObject> gimmicks = new List<GameObject>();

    private float maxPosX, minPosX, maxPosY, minPosY;
    // �I�u�W�F�N�g����N���b�N�ۂ͈̔�
    private const float POS_RANGE = 0.5f;

    [SerializeField]
    private GameObject debugPoint;


    // Start is called before the first frame update
    void Start()
    {
        // �S�Ă�GameObject�^��z��Ŏ擾���A���̗v�f�����J��Ԃ��M�~�b�N����������
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.tag != "Untagged" && obj.tag != "MainCamera")
            {
                gimmicks.Add(obj);
                //Debug.Log(obj.name);
            }
        }

        // �f�o�b�O�p�͈̓|�C���g�𐶐�����
        foreach (GameObject obj in gimmicks)
        {
            CalcuratePositionRange(obj);
            GenerateDebugPoint(maxPosX, minPosX, maxPosY, minPosY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �X�e�[�W��ҏW�ł���悤�ɂ���
        if(Input.GetKeyDown(KeyCode.RightAlt))
        {
            editStageFlg = !editStageFlg;
            if(editStageFlg) Debug.Log("�X�e�[�W�ҏW���[�h");
        }

        if(editStageFlg)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //  �N���b�N������ʂ̍��W���擾����
                clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            foreach (GameObject obj in gimmicks)
            {
                CalcuratePositionRange(obj);
                if (clickPos.x > minPosX && clickPos.x < maxPosX && clickPos.y > minPosY && clickPos.y < maxPosY)
                {
                    editObj = obj;
                    //Debug.Log(editObj.name + "�͈͓̔��ł�");
                }
            }

            if (editObj != null)
            {
                // �M�~�b�N�̑�����ύX����
            }
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

    /// <summary>
    /// �M�~�b�N�̃N���b�N�۔͈͂�\������
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
            //  �N���b�N������ʂ̍��W���擾����
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
            // ���ʂ̏Փ�
            case "Nail":
                normalReflectFlg = true;
                //Debug.Log("�B�ɏՓ˂��܂���");
                break;
            // �������锽��
            case "Balloon":
                reboundReflectFlg = true;
                //Debug.Log("���D�ɏՓ˂��܂���");
                break;
            // �z�����锽��
            case "Pillow":
                absorbReflectFlg = true;
                //Debug.Log("���ɏՓ˂��܂���");
                break;
        }
    }
}
