using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRobotMotion : MonoBehaviour{


    public void LockRobotMotion(Mibo.MiboMotorType miboMotorType)
    {
        //float headDegree01 = Mibo.getMotorPresentPossitionInDegree(miboMotorType);
        //if (headDegree01 > 5 || headDegree01 < -5)
        //{
        //    Mibo.setMotorPositionInDegree(miboMotorType, 0, 80f);
        //}
        StartCoroutine(IE_lockRobotMotion(miboMotorType));
    }

    IEnumerator IE_lockRobotMotion(Mibo.MiboMotorType miboMotorType)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);

            float headDegree01 = Mibo.getMotorPresentPossitionInDegree(miboMotorType);
            if (headDegree01 > 0.5f || headDegree01 < -0.5f)
            {
                Mibo.setMotorPositionInDegree(miboMotorType, 0, 80f);
            }
        }     
    }

}
