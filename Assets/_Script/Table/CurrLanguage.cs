using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrLanguage : MonoSingleton<CurrLanguage> {

    public static SystemLanguage currLanguage;

    protected  void Awake()
    {
        SetupAnimalDatabaseCurrLanguage();
    }

    public void SetupAnimalDatabaseCurrLanguage() {
        //判斷語言
        currLanguage = SystemLanguage.English;//Application.systemLanguage;
        Debug.Log(currLanguage);
    }
}
