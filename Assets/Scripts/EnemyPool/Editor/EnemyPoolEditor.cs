using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyPool))]
public class EnemyPoolEditor : Editor
{
    private EnemyPool _enemyPool;

    private void OnEnable()
    {
        _enemyPool = (EnemyPool)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
