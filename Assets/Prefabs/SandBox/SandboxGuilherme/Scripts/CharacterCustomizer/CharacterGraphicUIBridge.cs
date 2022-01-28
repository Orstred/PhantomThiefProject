using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGraphicUIBridge : MonoBehaviour
{
    public GraphicManager Character;




    //Dropdown menus
    public void ChangeGender(int G)
    {
       if (G == 0)
        {
            Character.Gender = gender.Male;
        }
       else if (G == 1)
        {
            Character.Gender = gender.Female;
        }

        Character.UpdateCostume();
    }

    public void ChangeProfession(int P)
    {
        if (P == 0)
        {
            Character.Profession = profession.PhantomThief;
        }
        else if (P == 1)
        {
            Character.Profession = profession.Police;
        }
        else if (P == 2)
        {
            Character.Profession = profession.Civilian;
        }
        else if (P == 3)
        {
            Character.Profession = profession.Miscellaneous;
        }
        Character.UpdateCostume();
    }




    public void NextHat()
    {
        if(Character.CurrentGenderCostumes().CurrentProfession().Length > Character.Hat_id +1  )
        {
            Character.Hat_id++;
            Character.UpdateCostume();
        }

    }
    public void PreviousHat()
    {
        if ( Character.Hat_id > 0 )
        {
            Character.Hat_id--;
            Character.UpdateCostume();
        }

    }

    
    public void NextHead()
    {
        if (Character.CurrentGenderCostumes().CurrentProfession().Length > Character.Head_id + 1)
        {
            Character.Head_id++;
            Character.UpdateCostume();
        }
    }
    public void PreviousHead()
    {
        if (Character.Head_id > 0)
        {
            Character.Head_id--;
            Character.UpdateCostume();
        }
    }


    public void NextBody()
    {
        if (Character.CurrentGenderCostumes().CurrentProfession().Length > Character.Body_id + 1)
        {
            Character.Body_id++;
            Character.UpdateCostume();
        }
    }
    public void PreviousBody()
    {
        if(Character.Body_id > 0)
        {
            Character.Body_id--;
        }
    }


    public void NextLegs()
    {
        if (Character.CurrentGenderCostumes().CurrentProfession().Length > Character.Legs_id + 1)
        {
            Character.Legs_id++;
            Character.UpdateCostume();
        }
    }
    public void PreviousLegs()
    {
        if (Character.Legs_id > 0)
        {
            Character.Legs_id--;
        }
    }
}
