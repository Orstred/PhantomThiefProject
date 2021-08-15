using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum Clothetype {PhantomThief, Police, Civilian, FireFighter, Medic }
public enum Gender {Male, Female }
public enum BodyType {Slim, Built, Chubby}


[System.Serializable]
public struct CurrentCostume
{
    public int Head_id;
    public int Body_id;
    public int Legs_id;
}

public class PlayerEntity : MonoBehaviour
{

    private GameObject _currenthead;
    private GameObject _currentbody;
    private GameObject _currentlegs;

    [BoxGroup("")]
    public CostumeInstances _Instances;

    [Foldout("PlayerBody")]
    public BodyType _BodyType;
    [Foldout("PlayerBody")]
    public Clothetype _Profession;
    [Foldout("PlayerBody")]
    public Gender _Gender;

    [BoxGroup("Current ID's")]
    public CurrentCostume _CurrentCostume;





    [Button]
    public void UpdateCharacterModel()
    {
        if(_currenthead != null)
        _currenthead.SetActive(false);
        if(_currentbody != null)
        _currentbody.SetActive(false);
        if(_currentlegs != null)
        _currentlegs.SetActive(false);

        if(_Profession == Clothetype.PhantomThief)
        {
            if(_BodyType == BodyType.Slim)
            {
                _Instances._PhantomThiefInstances.SlimCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._PhantomThiefInstances.SlimCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._PhantomThiefInstances.SlimCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
                _currenthead = _Instances._PhantomThiefInstances.SlimCostumes.Heads[_CurrentCostume.Head_id];
                _currentbody = _Instances._PhantomThiefInstances.SlimCostumes.Bodies[_CurrentCostume.Body_id];
                _currentlegs = _Instances._PhantomThiefInstances.SlimCostumes.Legs[_CurrentCostume.Legs_id];
            }
            else if (_BodyType == BodyType.Built)
            {
                _Instances._PhantomThiefInstances.BuiltCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._PhantomThiefInstances.BuiltCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._PhantomThiefInstances.BuiltCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
                _currenthead = _Instances._PhantomThiefInstances.BuiltCostumes.Heads[_CurrentCostume.Head_id];
                _currentbody = _Instances._PhantomThiefInstances.BuiltCostumes.Heads[_CurrentCostume.Head_id];
                _currentlegs = _Instances._PhantomThiefInstances.BuiltCostumes.Heads[_CurrentCostume.Head_id];
            }
            else if (_BodyType == BodyType.Chubby)
            {
                _Instances._PhantomThiefInstances.ChubbyCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._PhantomThiefInstances.ChubbyCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._PhantomThiefInstances.ChubbyCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
                _currenthead = _Instances._PhantomThiefInstances.ChubbyCostumes.Heads[_CurrentCostume.Head_id];
                _currentbody = _Instances._PhantomThiefInstances.ChubbyCostumes.Bodies[_CurrentCostume.Body_id];
                _currentlegs = _Instances._PhantomThiefInstances.ChubbyCostumes.Legs[_CurrentCostume.Legs_id];
            }
        }
        else if (_Profession == Clothetype.Police)
        {
            if (_BodyType == BodyType.Slim)
            {
                _Instances._PoliceCostumes.SlimCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._PoliceCostumes.SlimCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._PoliceCostumes.SlimCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Built)
            {
                _Instances._PoliceCostumes.BuiltCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._PoliceCostumes.BuiltCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._PoliceCostumes.BuiltCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Chubby)
            {
                _Instances._PoliceCostumes.ChubbyCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._PoliceCostumes.ChubbyCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._PoliceCostumes.ChubbyCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
        }
        else if (_Profession == Clothetype.Civilian)
        {
            if (_BodyType == BodyType.Slim)
            {
                _Instances._CivilianCostumes.SlimCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._CivilianCostumes.SlimCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._CivilianCostumes.SlimCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Built)
            {
                _Instances._CivilianCostumes.BuiltCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._CivilianCostumes.BuiltCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._CivilianCostumes.BuiltCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Chubby)
            {
                _Instances._CivilianCostumes.ChubbyCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._CivilianCostumes.ChubbyCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._CivilianCostumes.ChubbyCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
        }
        else if (_Profession == Clothetype.FireFighter)
        {
            if (_BodyType == BodyType.Slim)
            {
                _Instances._FireFighterCostumes.SlimCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._FireFighterCostumes.SlimCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._FireFighterCostumes.SlimCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Built)
            {
                _Instances._FireFighterCostumes.BuiltCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._FireFighterCostumes.BuiltCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._FireFighterCostumes.BuiltCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Chubby)
            {
                _Instances._FireFighterCostumes.ChubbyCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._FireFighterCostumes.ChubbyCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._FireFighterCostumes.ChubbyCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
        }
        else if (_Profession == Clothetype.Medic)
        {
            if (_BodyType == BodyType.Slim)
            {
                _Instances._MedicCostumes.SlimCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._MedicCostumes.SlimCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._MedicCostumes.SlimCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Built)
            {
                _Instances._MedicCostumes.BuiltCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._MedicCostumes.BuiltCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._MedicCostumes.BuiltCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
            else if (_BodyType == BodyType.Chubby)
            {
                _Instances._MedicCostumes.ChubbyCostumes.Heads[_CurrentCostume.Head_id].SetActive(true);
                _Instances._MedicCostumes.ChubbyCostumes.Bodies[_CurrentCostume.Body_id].SetActive(true);
                _Instances._MedicCostumes.ChubbyCostumes.Legs[_CurrentCostume.Legs_id].SetActive(true);
            }
        }

    }


}
