using UnityEngine;

[CreateAssetMenu(fileName = "Meteroite Database", menuName = "Scriptable Objects/Database/Meteorite Database")]
public class MeteoriteDatabaseSO : ScriptableObject
{
    [SerializeField] private Meteorite[] meteorites;

    public Meteorite[] Meteorites => meteorites;
}
