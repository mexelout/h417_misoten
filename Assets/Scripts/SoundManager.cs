using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    
    protected static SoundManager instance;
    public GameObject TitleScene;
	public GameObject GameScene;
	public GameObject ResultScene;

	private GameObject Current;
	private GameObject InstScene;
    
    /*
    //インスタンス生成
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (SoundManager)FindObjectOfType(typeof(SoundManager));

                if (instance == null)
                {
                    Debug.LogError("SoundManager Instance Error");
                }
            }

            return instance;
        }
    }*/
    
    //******************** メンバ変数宣言 ********************
    //++++++++++ プライベート ++++++++++
    private AudioSource BGMsource;                                 //BGM再生用デバイス
    private AudioSource[] SEsources = new AudioSource[16];         //SE再生用デバイス
    private AudioSource[] VoiceSources = new AudioSource[16];      //Voice再生用デバイス

    //++++++++++ プロテクト ++++++++++
    

    //++++++++++ パブリック ++++++++++
    public AudioClip[] BGM;         //BGM格納配列
    public AudioClip[] SE;          //SE格納配列
    public AudioClip[] Voice;       //Voice格納配列

    //======================================================================
    //メソッド名   ：Awake
    //役割         ：コンストラクタ的な
    //引数         ：void
    //作成者       ：Nomura Syouhei
    //======================================================================
    void Awake()
    {
        //******************** 変数宣言 ********************
        //GameObject[] SoundObject;       //シーン毎のSoundManager格納用

        //******************** オブジェクト取得処理 ********************
        /*
        //全てのSoundManagerオブジェクトを探索して取得する
        SoundObject = GameObject.FindGameObjectsWithTag("SoundManager");

        //オブジェクトを取得出来た場合
        if(SoundObject.Length > 1)
        {
            //既に存在している場合は削除
            Destroy(gameObject);
        }
        //取得しなかった場合
        else
        {
            //シーン遷移ではサウンド情報を破棄させない設定に
            DontDestroyOnLoad(gameObject);
        }
        */
        //******************** サウンドファイル登録処理 ********************
        BGMsource = gameObject.AddComponent<AudioSource>();
        BGMsource.loop = true;
        BGMsource.clip = null;

        //SEの登録数だけループする
        for(int nLoop = 0; nLoop < SEsources.Length; nLoop++)
        {
            //SE登録配列毎に、SEを取得してSE再生デバイスに割り当てる
            SEsources[nLoop] = gameObject.AddComponent<AudioSource>();
        }

        //Voiceの登録数だけループする
        for(int nLoop = 0; nLoop < VoiceSources.Length; nLoop++)
        {
            //Voice登録配列毎に、Voiceを取得してVoice再生デバイスに割り当てる
            VoiceSources[nLoop] = gameObject.AddComponent<AudioSource>();
        }
    }

    //======================================================================
    //メソッド名   ：Start
    //役割         ：実行時に呼び出されるメソッド
    //引数         ：void
    //作成者       ：Nomura Syouhei
    //======================================================================
	void Start()
    {
        InstScene = (GameObject)Instantiate(TitleScene);
        Current = TitleScene;
	}
	
    //======================================================================
    //メソッド名   ：Update
    //役割         ：更新メソッド
    //引数         ：void
    //作成者       ：Nomura Syouhei
    //======================================================================
	void Update()
    {
        
	}

    //======================================================================
    //メソッド名   ：PlayBGM
    //役割         ：指定したBGMを再生する
    //引数         ：(int nPlayNumber)      再生番号
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void PlayBGM(int nPlayNumber)
    {
        //指定された再生番号が0以上(マイナス値指定×)、且つBGMの登録最大数を上回っていなく、且つ再生中BGMの再生番号と同じ再生番号ではない場合
        if(0 <= nPlayNumber && BGM.Length > nPlayNumber && BGMsource.clip != BGM[nPlayNumber])
        {
            BGMsource.Stop();                   //BGMを停止
            BGMsource.clip = BGM[nPlayNumber];  //指定された再生番号のBGMを、BGM再生デバイスに設定
            BGMsource.Play();                   //BGMを再生
        }
        //上記条件に当てはまらなかった場合(再生番号の指定が間違っていた場合)
        else
        {
            //条件毎に原因を載せたエラーログを出力する
            if(0 > nPlayNumber)
            {
                //エラーログ出力 - 再生番号がマイナス値
                Debug.Log("(BGM)指定された再生番号がマイナス値です。 再生番号：" + nPlayNumber);
            }
            else if(BGM.Length <= nPlayNumber)
            {
                //エラーログ出力 - BGMの登録最大数を超える
                Debug.Log("(BGM)指定された再生番号は登録数を超えています。 再生番号：" + nPlayNumber + "　BGM登録数：" + BGM.Length);
            }
            else if(BGMsource.clip == BGM[nPlayNumber])
            {
                //エラーログ出力 - 同じBGMが既に再生されている
                Debug.Log("(BGM)指定された再生番号は既に再生中です。");
            }
            else
            {
                //エラーログ出力 - 不明
                Debug.Log("Unknown Error");
            }
        }

        //メソッドから抜ける
        return;
    }

    //======================================================================
    //メソッド名   ：StopBGM
    //役割         ：再生中のBGMを停止する
    //引数         ：null
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void StopBGM()
    {
        BGMsource.Stop();           //BGMを停止
        BGMsource.clip = null;      //BGM再生デバイスの再生BGM設定にnull代入
    }

    //======================================================================
    //メソッド名   ：PlaySE
    //役割         ：指定したSEを再生する
    //引数         ：(int nPlayNumber)      再生番号
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void PlaySE(int nPlayNumber)
    {
        //指定された再生番号が0以上(マイナス値指定×)、且つSEの登録最大数を上回っていない場合
        if(0 <= nPlayNumber && SE.Length > nPlayNumber)
        {
            //SE再生デバイス中から、再生中でないSEを探索する
            for(int nLoop = 0 ; nLoop < SEsources.Length ; nLoop ++)
            {
                //対象のSE再生デバイスが再生中ではない場合
                if(!(SEsources[nLoop].isPlaying))
                {
                    SEsources[nLoop].clip = SE[nPlayNumber];    //指定された再生番号のSEを、SE再生デバイスに設定
                    SEsources[nLoop].Play();                    //SEを再生

                    //メソッドから抜ける
                    return;
                }
            }
        }
        //上記条件に当てはまらなかった場合(再生番号の指定が間違っていた場合)
        else
        {
            //条件毎に原因を載せたエラーログを出力する
            if (0 > nPlayNumber)
            {
                //エラーログ出力 - 再生番号がマイナス値
                Debug.Log("(SE)指定された再生番号がマイナス値です。 再生番号：" + nPlayNumber);
            }
            else if (SE.Length <= nPlayNumber)
            {
                //エラーログ出力 - SEの登録最大数を超える
                Debug.Log("(SE)指定された再生番号は登録数を超えています。 再生番号：" + nPlayNumber + "　SE登録数：" + SE.Length);
            }
            else
            {
                //エラーログ出力 - 不明
                Debug.Log("Unknown Error");
            }
        }

        //メソッドから抜ける
        return;
    }

    //======================================================================
    //メソッド名   ：StopSE
    //役割         ：指定した再生中のSEを停止する
    //引数         ：(int nPlayNumber)      再生番号
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void StopSE(int nPlayNumber)
    {
        SEsources[nPlayNumber].Stop();        //SEを停止する
        SEsources[nPlayNumber].clip = null;   //SE再生デバイスの再生SE設定にnull代入
    }

    //======================================================================
    //メソッド名   ：StopAllSE
    //役割         ：全ての再生中のSEを停止する
    //引数         ：null
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void StopAllSE()
    {
        //SE再生デバイス分だけループする
        for(int nLoop = 0; nLoop < SEsources.Length; nLoop++)
        {
            SEsources[nLoop].Stop();        //SEを停止する
            SEsources[nLoop].clip = null;   //SE再生デバイスの再生SE設定にnull代入
        }
    }


    //======================================================================
    //メソッド名   ：PlayVoice
    //役割         ：指定したVoiceを再生する
    //引数         ：(int nPlayNumber)      再生番号
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void PlayVoice(int nPlayNumber)
    {
        //指定された再生番号が0以上(マイナス値指定×)、且つVoiceの登録最大数を上回っていない場合
        if(0 <= nPlayNumber && Voice.Length > nPlayNumber)
        {
            //Voice再生デバイス中から、再生中でないVoiceを探索する
            for(int nLoop = 0; nLoop < VoiceSources.Length; nLoop++)
            {
                //対象のVoice再生デバイスが再生中ではない場合
                if(!(VoiceSources[nLoop].isPlaying))
                {
                    VoiceSources[nLoop].clip = Voice[nPlayNumber];    //指定された再生番号のVoiceを、Voice再生デバイスに設定
                    VoiceSources[nLoop].Play();                    //Voiceを再生

                    //メソッドから抜ける
                    return;
                }
            }
        }
        //上記条件に当てはまらなかった場合(再生番号の指定が間違っていた場合)
        else
        {
            //条件毎に原因を載せたエラーログを出力する
            if(0 > nPlayNumber)
            {
                //エラーログ出力 - 再生番号がマイナス値
                Debug.Log("(Voice)指定された再生番号がマイナス値です。 再生番号：" + nPlayNumber);
            }
            else if(Voice.Length <= nPlayNumber)
            {
                //エラーログ出力 - Voiceの登録最大数を超える
                Debug.Log("(Voice)指定された再生番号は登録数を超えています。 再生番号：" + nPlayNumber + "　Voice登録数：" + Voice.Length);
            }
            else
            {
                //エラーログ出力 - 不明
                Debug.Log("Unknown Error");
            }
        }
    }

    //======================================================================
    //メソッド名   ：StopVoice
    //役割         ：指定した再生中のVoiceを停止する
    //引数         ：(int nPlayNumber)      再生番号
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void StopVoice(int nPlayNumber)
    {
        VoiceSources[nPlayNumber].Stop();        //Voiceを停止する
        VoiceSources[nPlayNumber].clip = null;   //Voice再生デバイスの再生Voice設定にnull代入
    }

    //======================================================================
    //メソッド名   ：StopAllVoice
    //役割         ：全ての再生中のVoiceを停止する
    //引数         ：null
    //作成者       ：Nomura Syouhei
    //======================================================================
    public void StopAllVoice()
    {
        //Voice再生デバイス分だけループする
        for(int nLoop = 0; nLoop < VoiceSources.Length; nLoop++)
        {
            VoiceSources[nLoop].Stop();        //Voiceを停止する
            VoiceSources[nLoop].clip = null;   //Voice再生デバイスの再生Voice設定にnull代入
        }
    }

    public void NextScene()
    {
        Object.Destroy(InstScene);

        if (Current == TitleScene)
        {
            InstScene = (GameObject)Instantiate(GameScene);
            Current = GameScene;

            return;
        }
        if (Current == GameScene)
        {
            InstScene = (GameObject)Instantiate(ResultScene);
            Current = ResultScene;
            
            return;
        }
        if (Current == ResultScene)
        {
            InstScene = (GameObject)Instantiate(TitleScene);
            Current = TitleScene;
            
            return;
        }
    }
}
