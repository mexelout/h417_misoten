using UnityEngine;
using System;

public class CameraFade : MonoBehaviour {   
	public static CameraFade Create() {
		return new GameObject("CameraFade").AddComponent<CameraFade>();
	}

	void Awake() {
		init();
	}

	public GUIStyle m_BackgroundStyle = new GUIStyle();						// Style for background tiling
	public Texture2D m_FadeTexture;											// 1x1 pixel texture used for fading
	public Color m_CurrentScreenOverlayColor = new Color(0,0,0,0);			// default starting color: black and fully transparrent
	public Color m_TargetScreenOverlayColor = new Color(0,0,0,0);			// default target color: black and fully transparrent
	public Color m_DeltaColor = new Color(0,0,0,0);							// the delta-color is basically the "speed / second" at which the current color should change
	public int m_FadeGUIDepth = -1000;										// make sure this texture is drawn on top of everything

	public float m_FadeDelay = 0;
	public Action m_OnFadeFinish = null;

	// Initialize the texture, background-style and initial color:
	public void init()
	{		
		m_FadeTexture = new Texture2D(1, 1);        
		m_BackgroundStyle.normal.background = m_FadeTexture;
	}

	// Draw the texture and perform the fade:
	void OnGUI()
	{   
		// If delay is over...
		if( Time.time > m_FadeDelay )
		{
			// If the current color of the screen is not equal to the desired color: keep fading!
			if (m_CurrentScreenOverlayColor != m_TargetScreenOverlayColor)
			{			
				// If the difference between the current alpha and the desired alpha is smaller than delta-alpha * deltaTime, then we're pretty much done fading:
				if (Mathf.Abs(m_CurrentScreenOverlayColor.a - m_TargetScreenOverlayColor.a) < Mathf.Abs(m_DeltaColor.a) * Time.deltaTime)
				{
					m_CurrentScreenOverlayColor = m_TargetScreenOverlayColor;
					SetScreenOverlayColor(m_CurrentScreenOverlayColor);
					m_DeltaColor = new Color( 0,0,0,0 );

					if( m_OnFadeFinish != null )
						m_OnFadeFinish();

					Die();
				}
				else
				{
					// Fade!
					SetScreenOverlayColor(m_CurrentScreenOverlayColor + m_DeltaColor * Time.deltaTime);
				}
			}
		}
		// Only draw the texture when the alpha value is greater than 0:
		if (m_CurrentScreenOverlayColor.a > 0)
		{			
			GUI.depth = m_FadeGUIDepth;
			GUI.Label(new Rect(-10, -10, Screen.width + 10, Screen.height + 10), m_FadeTexture, m_BackgroundStyle);
		}
	}


	/// <summary>
	/// Sets the color of the screen overlay instantly.  Useful to start a fade.
	/// </summary>
	/// <param name='newScreenOverlayColor'>
	/// New screen overlay color.
	/// </param>
	private void SetScreenOverlayColor(Color newScreenOverlayColor)
	{
		m_CurrentScreenOverlayColor = newScreenOverlayColor;
		m_FadeTexture.SetPixel(0, 0, m_CurrentScreenOverlayColor);
		m_FadeTexture.Apply();
	}

	/// <summary>
	/// Starts the fade from color newScreenOverlayColor. If isFadeIn, start fully opaque, else start transparent.
	/// </summary>
	/// <param name='newScreenOverlayColor'>
	/// Target screen overlay Color.
	/// </param>
	/// <param name='fadeDuration'>
	/// Fade duration.
	/// </param>
	public void StartAlphaFade(Color newScreenOverlayColor, bool isFadeIn, float fadeDuration )
	{
		if (fadeDuration <= 0.0f)		
		{
			SetScreenOverlayColor(newScreenOverlayColor);
		}
		else					
		{
			if( isFadeIn )
			{
				m_TargetScreenOverlayColor = new Color( newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0 );
				SetScreenOverlayColor( newScreenOverlayColor );
			} else {
				m_TargetScreenOverlayColor = newScreenOverlayColor;
				SetScreenOverlayColor( new Color( newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0 ) );
			}

			m_DeltaColor = (m_TargetScreenOverlayColor - m_CurrentScreenOverlayColor) / fadeDuration;	
		}
	}

	/// <summary>
	/// Starts the fade from color newScreenOverlayColor. If isFadeIn, start fully opaque, else start transparent, after a delay.
	/// </summary>
	/// <param name='newScreenOverlayColor'>
	/// New screen overlay color.
	/// </param>
	/// <param name='fadeDuration'>
	/// Fade duration.
	/// </param>
	/// <param name='fadeDelay'>
	/// Fade delay.
	/// </param>
	public void StartAlphaFade(Color newScreenOverlayColor, bool isFadeIn, float fadeDuration, float fadeDelay )
	{
		if (fadeDuration <= 0.0f)		
		{
			SetScreenOverlayColor(newScreenOverlayColor);
		}
		else					
		{
			m_FadeDelay = Time.time + fadeDelay;			

			if( isFadeIn )
			{
				m_TargetScreenOverlayColor = new Color( newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0 );
				SetScreenOverlayColor( newScreenOverlayColor );
			} else {
				m_TargetScreenOverlayColor = newScreenOverlayColor;
				SetScreenOverlayColor( new Color( newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0 ) );
			}

			m_DeltaColor = (m_TargetScreenOverlayColor - m_CurrentScreenOverlayColor) / fadeDuration;
		}
	}

	/// <summary>
	/// Starts the fade from color newScreenOverlayColor. If isFadeIn, start fully opaque, else start transparent, after a delay, with Action OnFadeFinish.
	/// </summary>
	/// <param name='newScreenOverlayColor'>
	/// New screen overlay color.
	/// </param>
	/// <param name='fadeDuration'>
	/// Fade duration.
	/// </param>
	/// <param name='fadeDelay'>
	/// Fade delay.
	/// </param>
	/// <param name='OnFadeFinish'>
	/// On fade finish, doWork().
	/// </param>
	public void StartAlphaFade(Color newScreenOverlayColor, bool isFadeIn, float fadeDuration, float fadeDelay, Action OnFadeFinish ) {
		if (fadeDuration <= 0.0f)		
		{
			SetScreenOverlayColor(newScreenOverlayColor);
		}
		else					
		{
			m_OnFadeFinish = OnFadeFinish;
			m_FadeDelay = Time.time + fadeDelay;

			if( isFadeIn )
			{
				m_TargetScreenOverlayColor = new Color( newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0 );
				SetScreenOverlayColor( newScreenOverlayColor );
			} else {
				m_TargetScreenOverlayColor = newScreenOverlayColor;
				SetScreenOverlayColor( new Color( newScreenOverlayColor.r, newScreenOverlayColor.g, newScreenOverlayColor.b, 0 ) );
			}
			m_DeltaColor = (m_TargetScreenOverlayColor - m_CurrentScreenOverlayColor) / fadeDuration;
		}
	}

	void Die() {
		Destroy(gameObject);
	}
}
