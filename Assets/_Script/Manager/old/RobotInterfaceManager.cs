using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// 跟機器人有關的介面部分(之前應急用，目前暫時不使用這個class)
/// </summary>
public class RobotInterfaceManager : MonoSingleton<RobotInterfaceManager>
{
    private const string mMotionCSVFileName = "motion";
    private const string mTTSCSVFileName = "tts";

    /// <summary>
    /// TTs用的Dict
    /// </summary>
    private Dictionary<ETTsInfo, string> TTsInfoDict = new Dictionary<ETTsInfo, string>();
    /// <summary>
    /// Motion用的Dict
    /// </summary>
    private Dictionary<EMotionInfo, string> MotionInfoDict = new Dictionary<EMotionInfo, string>();

    /// <summary>
    /// 目前的系統語言
    /// </summary>
    private SystemLanguage mCurrLanguage;

    private void Start()
    {
        //mCurrLanguage = Application.systemLanguage;
        //mCurrLanguage = SystemLanguage.Japanese;
        //SetMotionDict();
        //SetTTsDict();
    }

    #region set

    private void SetTTsDict()
    {
        //抓取對應語言
        string language = Application.systemLanguage.ToString();
        TextAsset ttsAsset = Resources.Load(mTTSCSVFileName, typeof(TextAsset)) as TextAsset;
        //Debug.LogError("motionAsset is null : " + (motiongAsset == null) + "," + motiongAsset);
        string[] lineArray = ttsAsset.text.Split("\r"[0]);
        string[][] Array;          //读取每一行的内容  
        //创建二维数组  
        Array = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            Array[i] = lineArray[i].Split(',');
            if (Array[i].Length == 3)
                try
                {
                    int intResult = 0;
                    // 找出
                    if (int.TryParse(Array[i][0], out intResult))
                    {
                        if (intResult > 0)
                        {
                            ETTsInfo info = (ETTsInfo)intResult;
                            
                            string langString =  Array[i][1]; ;
                            if (mCurrLanguage == SystemLanguage.Japanese)
                                langString = Array[i][2];

                            Debug.Log("Add ETTs to dict : " + info + " ," + langString + ", mCurrLanguage: "+ mCurrLanguage.ToString() );
                            TTsInfoDict.Add(info, langString);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
        }
    }

    /// <summary>
    /// 設定MotionDict
    /// </summary>
    private void SetMotionDict()
    {
        //  TextAsset textAsset = Resources.Load<TextAsset>("MyAsset/MyText");
        TextAsset motiongAsset = Resources.Load(mMotionCSVFileName , typeof(TextAsset)) as TextAsset;
        //Debug.LogError("motionAsset is null : " + (motiongAsset == null) + "," + motiongAsset);
        string[] lineArray = motiongAsset.text.Split("\r"[0]);
        string[][] Array;          //读取每一行的内容  
        //创建二维数组  
        Array = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            Array[i] = lineArray[i].Split(',');
            if (Array[i].Length == 2)
            try
            {
                int intResult = 0;
                // 找出
                if(int.TryParse(Array[i][0], out intResult)) //第一個欄位無法轉成int不執行(false)
                {
                    if(intResult > 0)
                    {
                        EMotionInfo info = (EMotionInfo)intResult;
                        Debug.Log("Add motion to dict : " + info + " , " + Array[i][1] + " , " + Array[i][0]);
                        MotionInfoDict.Add(info, Array[i][1]);
                    }
                }
            }
            catch(Exception e)
            {
                    Debug.LogWarning(e);
            }
        }

    }

    #endregion set

    /// <summary>
    /// 撥放TTS
    /// </summary>
    /// <param name="info"></param>
    public void PlayTTs(ETTsInfo info)
    {
        string value = TTsInfoDict[info];
        Debug.Log("PlayTTs, info: " + info +", value: "+ value);
        Mibo.stopTTS();
        Mibo.startTTS(value);
    }

    /// <summary>
    /// 撥放Motion
    /// </summary>
    /// <param name="info"></param>
    public void PlayMotion(EMotionInfo info)
    {
        string value = MotionInfoDict[info];
        Debug.Log("PlayMotion, info: " + info + ", value:"+ value);
        //Mibo.motionStop();
        Mibo.motionPlay(value);
    }

    public static T ParseEnum<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}

/// <summary>
/// TTS對應的Enum
/// </summary>
public enum ETTsInfo
{
    /// <summary>
    /// 1.進入遊戲（標題畫面）
    /// </summary>
    StartGame = 1,
    /// <summary>
    /// 2.教學（第一關有教學卡）
    /// </summary>
    Teach = 2,
    /// <summary>
    /// 3.第一大關任務說明（挖土那三關）
    /// </summary>
    Mission_Map_001 = 3,
    /// <summary>
    /// 4.第二大關任務說明（躲雨那三關）
    /// </summary>
    Mission_Map_002 = 4,
    /// <summary>
    /// 5.第三大關任務說明（挖東西然後找狗狗那三關）
    /// </summary>
    Mission_Map_003 = 5,
    /// <summary>
    /// 6.結算-成功（成功完成任務）
    /// </summary>
    Result_Success = 6,
    /// <summary>
    /// 7.結算-失敗（沒有完成任務
    /// </summary>
    Result_Fail = 7,
}

/// <summary>
/// Motion對應的Enum
/// </summary>
public enum EMotionInfo
{
    /// <summary>
    /// 1.進入遊戲（標題畫面）
    /// </summary>
    StartGame = 1,
    /// <summary>
    /// 2.教學（第一關有教學卡）
    /// </summary>
    Teach = 2,
    /// <summary>
    /// 3.第一大關任務說明（挖土那三關）
    /// </summary>
    Mission_Map_001 = 3,
    /// <summary>
    /// 4.第二大關任務說明（躲雨那三關）
    /// </summary>
    Mission_Map_002 = 4,
    /// <summary>
    /// 5.第三大關任務說明（挖東西然後找狗狗那三關）
    /// </summary>
    Mission_Map_003 = 5,
    /// <summary>
    /// 6.結算-成功（成功完成任務）
    /// </summary>
    Result_Success = 6,
    /// <summary>
    /// 7.結算-失敗（沒有完成任務
    /// </summary>
    Result_Fail = 7,
    /// <summary>
    /// 機器人移動
    /// </summary>
    Robot_Walk = 8,
    /// <summary>
    /// 挖土
    /// </summary>
    Robot_Dig_hole = 9,
    /// <summary>
    /// 遮雨
    /// </summary>
    Robot_Unbrella = 10,
    /// <summary>
    /// 拿東西
    /// </summary>
    Robot_Take_Something = 11,
}