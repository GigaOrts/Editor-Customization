using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyPool))]
public class EnemyPoolEditor : Editor
{
    private EnemyPool _enemyPool;
    private SerializedProperty _spawnedEnemies;

    private void OnEnable()
    {
        _enemyPool = (EnemyPool)target;
        _spawnedEnemies = serializedObject.FindProperty("_spawnedEnemies");
    }

    public override void OnInspectorGUI()
    {
        
    }
}
