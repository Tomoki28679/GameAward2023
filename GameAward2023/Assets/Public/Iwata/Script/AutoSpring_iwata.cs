using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpring_iwata : JankBase_iwata
{
    public float waitTime = 2f; // �ŏ��l�ɒB������̑ҋ@����
    public float maxHeight = 2f; // �V�����_�[�̍ő卂��
    public float minHeight = 0.5f; // �V�����_�[�̍ŏ�����
    public float increasingSpeed = 3f; // �L�k���x�i�L�т�Ƃ��j
    public float decreasingSpeed = 1f; // �L�k���x�i�k�ނƂ��j

    private float currentHeight; // ���݂̍���
    private bool increasing = true; // ���������ǂ�����\���t���O
    private bool waiting = false; // �ҋ@�����ǂ�����\���t���O
    private float timeSinceMinHeight = 0f; // �ŏ��l�ɒB���Ă���̌o�ߎ��Ԃ�ێ�����ϐ�

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }
    
    public override void work()
    {
        // ������ύX
        if (increasing && !waiting)
        {
            currentHeight += increasingSpeed * Time.deltaTime; // ������
        }
        else if (!increasing && !waiting)
        {
            currentHeight -= decreasingSpeed * Time.deltaTime; // ������
        }

        // �ő卂���ɒB�����ꍇ�A�������̃t���O�𔽓]������
        if (currentHeight >= maxHeight)
        {
            increasing = false;
        }
        // �ŏ������ɒB�����ꍇ�A�������̃t���O�𔽓]�����A�ҋ@���̃t���O�𗧂Ă�
        else if (currentHeight <= minHeight)
        {
            increasing = true;
            waiting = true;
        }

        // �ŏ��l�ɒB������A��莞�ԑҋ@���Ă���ĂѐL�юn�߂�
        if (waiting)
        {
            timeSinceMinHeight += Time.deltaTime;
            if (timeSinceMinHeight >= waitTime)
            {
                waiting = false;
                timeSinceMinHeight = 0f;
            }
        }

        // �V�����_�[�̃X�P�[����ύX����
        transform.Find("Povit").localScale = new Vector3(1f, currentHeight, 1f);
    }

    /// <summary>
    /// �e�W�����N�̃p�����[�^���擾����
    /// </summary>
    public override List<float> GetParam()
    {
        List<float> list = new List<float>();

        list.Add(waitTime);

        return list;
    }

    /// <summary>
    /// �p�����[�^�[�z�u
    /// </summary>
    /// <param name="paramList"></param>
    public override void SetParam(List<float> paramList)
    {
        waitTime = paramList[0];
    }
}