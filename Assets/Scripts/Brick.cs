using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
	public AudioClip crack;
	public Sprite[] hitSprites;
	public static int breakableCount = 0;
	private bool isBreakable;
	private int timesHit;
	private LevelManager levelManager;
	public GameObject smoke;

	bool NoSprite ()
	{
		if (this.GetComponent<SpriteRenderer> ().sprite == null) {
			return true;
		} else {
			return false;
		}
	}
	
	void Start ()
	{
		if (NoSprite ()) {
			Debug.LogError ("No sprite attached to bricks");
		}
		isBreakable = (this.tag == "breakable");
		//keep track of breakable bricks
		if (isBreakable) {
			breakableCount++;
		}
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		timesHit = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	void OnCollisionEnter2D (Collision2D collision)
	{
		AudioSource.PlayClipAtPoint (crack, transform.position, .03f);
		if (isBreakable) {
			HandleHits ();
		}
	}
	
	void HandleHits ()
	{
		int maxHits;
		timesHit++;
		maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits) {
			GameObject smokePuff = (GameObject)Instantiate (smoke, this.transform.position, Quaternion.identity);
			smokePuff.particleSystem.startColor = this.GetComponent<SpriteRenderer> ().color;
			breakableCount--;
			levelManager.BrickDestroyed ();
			Destroy (gameObject);
		} else {
			LoadSprites ();
		}
	}
	
	void LoadSprites ()
	{
		int spriteIndex = timesHit - 1;
		if (hitSprites [spriteIndex]) {
			this.GetComponent<SpriteRenderer> ().sprite = hitSprites [spriteIndex];
		}
	}
	
}