using UnityEngine;
using System.Collections;

public class HUDFPS : MonoBehaviour
{
  public float updateInterval = 0.5F;

  private float accum = 0; // FPS accumulated over the interval
  private int frames = 0; // Frames drawn over the interval
  private float timeleft; // Left time for current interval

  private UnityEngine.UI.Text _text;
#if UNITY_ANDROID //&& !UNITY_EDITOR
	void Start()
	{
		_text = GetComponent<UnityEngine.UI.Text> ();
		if(!_text )
		{
			Debug.Log("UtilityFramesPerSecond needs a UI Text component!");
			enabled = false;
			return;
		}
		timeleft = updateInterval;  
	}
	
	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 )
		{
			// display two fractional digits (f2 format)
			float fps = accum/frames;
			string format = System.String.Format("{0:F2} FPS",fps);
			_text.text = format;
			
			if(fps < 30)
				_text.color = Color.yellow;
			else 
				if(fps < 10)
					_text.color = Color.red;
			else
				_text.color = Color.green;
			//	DebugConsole.Log(format,level);
			timeleft = updateInterval;
			accum = 0.0F;
			frames = 0;
		}
	}
#endif
}