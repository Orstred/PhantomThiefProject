using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class ToolWheelUI : MonoBehaviour
{

    public RectTransform[] Actives;
    public RectTransform Knob;

    public float speed;
    public float MaxX;
    public float MaxY;

    float angle;
    float x;
    float y;





    private void Start()
    {
        foreach(RectTransform r in Actives)
        {
            r.gameObject.SetActive(false);
        }
        Knob.GetComponent<Image>().enabled = false;
    }



    private void Update()
    {
        #region ballmove
        x += Input.GetAxis("Mouse X") * Time.deltaTime * speed;
        y -= Input.GetAxis("Mouse Y") * Time.deltaTime * speed;


        if (Vector3.Distance(Vector3.zero, Knob.localPosition) > MaxY)
        {
            Knob.localPosition = Vector3.Lerp(Knob.localPosition, Vector3.zero, .1f);
            y = Knob.localPosition.y;
            x = Knob.localPosition.x;
        }


        y = Mathf.Clamp(y, -MaxY, MaxY);
        x = Mathf.Clamp(x, -MaxX, MaxX);


        Knob.localPosition = new Vector3(x, y, 0);
        #endregion


        // Stores an angle based on the knob position
        angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;


        
        if (angle> -62 && angle<1)
        {
           SelectingButton(Actives[0].gameObject);
        }
        else if(angle>.9f && angle< 50)
        {
            SelectingButton(Actives[1].gameObject);
        }
        else if (angle > 50f && angle < 85)
        {
            SelectingButton(Actives[2].gameObject);
        }
        else if (angle > 85f && angle < 128)
        {
            SelectingButton(Actives[3].gameObject);
        }
        else if (angle > 128f && angle < 179)
        {
            SelectingButton(Actives[4].gameObject);
        }
        else if (angle < -128 && angle > -179)
        {
            SelectingButton(Actives[5].gameObject);
        }
        else if (angle < -85 && angle > -128)
        {
            SelectingButton(Actives[6].gameObject);
        }
        else if (angle < -50 && angle > -85)
        {
            SelectingButton(Actives[7].gameObject);
        }
        else
        {
            foreach (RectTransform r in Actives)
            {
                r.gameObject.SetActive(false);
            }
        }







     

        
    }



   
    public void SelectingButton(GameObject g)
    {
        foreach (RectTransform r in Actives)
        {
            r.gameObject.SetActive(false);
        }
        g.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Mouse0) && g.GetComponent<UIItem>() != null)
        {
            Debug.Log("Used: " + g.name);

            UIItem i = new UIItem();
            g.gameObject.TryGetComponent<UIItem>(out i);
            i.OnInteract();

        }
   
    }

    private void OnDisable()
    {
        foreach (RectTransform r in Actives)
        {
            if (r.gameObject.activeSelf && r.GetComponent<UIItem>() != null)
            {
                UIItem i = null;
                r.TryGetComponent<UIItem>(out i);
                i.OnInteract();
            }
        }

    }
}
