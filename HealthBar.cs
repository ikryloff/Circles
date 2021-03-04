using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    private SpriteRenderer sprite;
    public Color32 highColor;
    public Color32 lowColor;
    public float height;
    public float counter = 1f;
    private void Awake()
    {
        bar = transform.Find ("Bar");
        sprite = bar.Find ("BarSprite").GetComponent<SpriteRenderer> ();
    }

    public void SetHBSize( float size )
    {
        if ( size <= 0 )
        {
            bar.localScale = new Vector3 (0f, 1f);
            SetHBColor (0f);
            return;
        }
        bar.localScale = new Vector3 (size, 1f);
        SetHBColor (size);
    }

    public void SetHBColor( float sizeNorm )
    {
        sprite.color = Color32.Lerp (lowColor, highColor, sizeNorm);
    }

   
}
