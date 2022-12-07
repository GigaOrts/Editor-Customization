using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(EnemyPool))]
public class EnemyPoolEditor : Editor
{
    private EnemyPool _enemyPool;
    private SerializedProperty _spawnedEnemies;

    private void OnEnable()
    {
        _enemyPool = (EnemyPool)target;
        _spawnedEnemies = serializedObject.FindProperty("_spawnedEnemies");

        if (_enemyPool.SpawnDatas == null || _enemyPool.SpawnDatas.Count == 0)
        {
            _enemyPool.InitializeSpawnData();
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(_spawnedEnemies);

        SpawnData spawnData = _enemyPool.SpawnDatas[0];
        GUILayout.Label(spawnData.EnemiesType.ToString());

        spawnData.IsRandomCount = EditorGUILayout.Toggle("Random Count", spawnData.IsRandomCount);
        if (spawnData.IsRandomCount)
            spawnData.Range = EditorGUILayout.Vector2IntField("Random Range", spawnData.Range);
        else
            spawnData.Count = EditorGUILayout.IntField("Count", spawnData.Count);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_enemyPool);
            EditorSceneManager.MarkSceneDirty(_enemyPool.gameObject.scene);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
