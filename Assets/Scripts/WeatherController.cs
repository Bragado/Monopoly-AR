using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System;

public class WeatherController : MonoBehaviour {

	private const string API_KEY = "86b46e48eff6f9add09f62cfee0d8e42";
    private const float API_CHECK_MAXTIME = 5 * 60.0f; //10 minutes
	public GameObject RainSystem;
	public GameObject CloudSystem;
    public string latitude = "41.17794010000001";
	public string longitude = "-8.597687599999972";
    private float apiCheckCountdown = API_CHECK_MAXTIME;
    void Start()
    {
		CheckWeatherStatus();
    }
    void Update()
    {
    	apiCheckCountdown -= Time.deltaTime;
        if (apiCheckCountdown <= 0)
        {
            CheckWeatherStatus();
            apiCheckCountdown = API_CHECK_MAXTIME;
        }
    }

	public void CheckWeatherStatus()
    {
    	string weatherStatus = GetWeather().list[0].weather[0].main;

		switch(weatherStatus){
			case "Rain":
				Debug.Log("Rain");
				RainSystem.SetActive(true);
				CloudSystem.SetActive(false);
				break;
			case "Thunderstorm":
				Debug.Log("Thunderstorm");
				RainSystem.SetActive(true);
				CloudSystem.SetActive(false);
				break;
			case "Clouds":
				Debug.Log("Clouds");
				RainSystem.SetActive(false);
				CloudSystem.SetActive(true);
				break;
			case "Clear":
				Debug.Log("Clear");
				RainSystem.SetActive(false);
				CloudSystem.SetActive(false);
				break;
			default:
				Debug.Log("Other");
				RainSystem.SetActive(false);
				CloudSystem.SetActive(false);
				break;
		}
    }

    private WeatherInfo GetWeather()
    {
		ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
		HttpWebRequest request = 
        (HttpWebRequest)WebRequest.Create(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=86b46e48eff6f9add09f62cfee0d8e42",
		latitude, longitude));	
		HttpWebResponse response = (HttpWebResponse)(request.GetResponse());
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);

        return info;
    }

	public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
    	bool isOk = true;
		// If there are errors in the certificate chain, look at each error to determine the cause.
		if (sslPolicyErrors != SslPolicyErrors.None) {
			for (int i=0; i<chain.ChainStatus.Length; i++) {
				if (chain.ChainStatus [i].Status != X509ChainStatusFlags.RevocationStatusUnknown) {
					chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
					chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
					chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan (0, 1, 0);
					chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
					bool chainIsValid = chain.Build ((X509Certificate2)certificate);
					if (!chainIsValid) {
						isOk = false;
					}
				}
			}
    	}
    	return isOk;
	}

	[System.Serializable]
	public class WeatherInfo {
		public int cnt;
    	public string message;
    	public List<Weather> list;
	}

	[System.Serializable]
	public class Weather {
		//public int id;
    	public string dt;
		public List<WeatherResume> weather;
	}

	[System.Serializable]
	public class WeatherResume {
    	public string main;
	}
}
