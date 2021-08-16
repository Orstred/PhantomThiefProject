using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public enum gender {Male, Female }


public class GraphicManager : MonoBehaviour
{
    #region Instances
    [Foldout("Instances")]
    public GraphicList FemaleCostumes;
    [Foldout("Instances")]
    public GraphicList MaleCostumes;
    #endregion

    #region Graphic ID
    [Foldout("GraphicID")]
    public gender Gender;
    [Foldout("GraphicID")]
    public int Hat_id;
    [Foldout("GraphicID")]
    public int Head_id;
    [Foldout("GraphicID")]
    public int Body_id;
    [Foldout("GraphicID")]
    public int Legs_id;
    #endregion

    #region Local Variables
    public GameObject _currentHat;
    public GameObject _currentHead;
    public GameObject _currentBody;
    public GameObject _currentLegs;
    #endregion






    [Button]
    public void ChangeCostume()
    {

        TurnHatOn(Hat_id);
        TurnHeadOn(Head_id);
        TurnBodyOn(Body_id);
        TurnLegsOn(Legs_id);

    }


    void TurnHatOn(int _id)
    {
        if(MaleCostumes.CurrentProfession().Length > _id)
        {
            if (Gender == gender.Male)
            {
                _currentHat.SetActive(false);
                _currentHat = MaleCostumes.CurrentProfession()[_id].Hat;
                _currentHat.SetActive(true);
            }

            else if (Gender == gender.Female)
            {
                _currentHat.SetActive(false);
                _currentHat = FemaleCostumes.CurrentProfession()[_id].Hat;
                _currentHat.SetActive(true);
            }
        }
        else { Debug.Log("Head out of range"); }



    }
    void TurnHeadOn(int _id)
    {

        if (MaleCostumes.CurrentProfession().Length > _id)
        {
            if (Gender == gender.Male)
            {
                _currentHead.SetActive(false);
                _currentHead = MaleCostumes.CurrentProfession()[_id].Head;
                _currentHead.SetActive(true);
            }

            else if (Gender == gender.Female)
            {
                _currentHead.SetActive(false);
                _currentHead = FemaleCostumes.CurrentProfession()[_id].Head;
                _currentHead.SetActive(true);
            }
        }
        else { Debug.Log("Head out of range"); }
        
    }

    void TurnBodyOn(int _id)
    {
        if (MaleCostumes.CurrentProfession().Length > _id)
        {
            if (Gender == gender.Male)
            {
                _currentBody.SetActive(false);
                _currentBody = MaleCostumes.CurrentProfession()[_id].Body;
                _currentBody.SetActive(true);
            }

            else if (Gender == gender.Female)
            {
                _currentBody.SetActive(false);
                _currentBody = FemaleCostumes.CurrentProfession()[_id].Body;
                _currentBody.SetActive(true);
            }
        }
        else { Debug.Log("Head out of range"); }

    }
    void TurnLegsOn(int _id)
    {
        if (MaleCostumes.CurrentProfession().Length > _id)
        {
            if (Gender == gender.Male)
            {
                _currentLegs.SetActive(false);
                _currentLegs = MaleCostumes.CurrentProfession()[_id].Legs;
                _currentLegs.SetActive(true);
            }

            else if (Gender == gender.Female)
            {
                _currentLegs.SetActive(false);
                _currentLegs = FemaleCostumes.CurrentProfession()[_id].Legs;
                _currentLegs.SetActive(true);
            }
        }
        else { Debug.Log("Head out of range"); }
    }


}
