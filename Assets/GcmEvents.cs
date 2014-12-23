using UnityEngine;
using System.Collections;

public class GcmEvents : MonoBehaviour {
	string m_regid = "";

	/// <summary>
	/// プラグインを呼び出しレジストレーションIDを取得します
	/// </summary>
	void Start () {
		Debug.Log ("***** Start UnityGCMPluginSample");
		var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
		var appContext = activity.Call<AndroidJavaObject>("getApplicationContext");
		
		Debug.Log ("***** get RegistrationId");
		var registrar = new AndroidJavaClass ("info.snaka.unitygcmpluginlib.GcmRegistrar");
		
		registrar.CallStatic ("clearCache", new object[] { appContext });
		
		string registrationId = registrar.CallStatic<string> ("getRegistrationId", new object[] { appContext });
		
		if (!string.IsNullOrEmpty (registrationId))
		{
			Debug.Log ("***** RegistrationId:[" + registrationId + "]");
		}
		else
		{
			// レジストレーションIDが取得できなかった場合はバックグラウンドでIDの取得を行う
			Debug.Log ("***** id empty");
			registrar.CallStatic("registerInBackground", new object[]{ appContext });
		}
	}
	
	
	/// <summary>
	/// バックグラウンドでのID取得が完了したらこのメソッドが呼び出される
	/// </summary>
	/// <param name="registerId">Register identifier.</param>
	public void OnRegister(string registerId) {
		Debug.Log ("##### RegisterId: " + registerId);
		m_regid = registerId;
	}


	/// <summary>
	/// 取得したレジストレーションIDを画面に表示する
	/// </summary>
	void OnGUI() {
		GUILayout.TextArea(m_regid, GUILayout.ExpandWidth(true));
	}
}
