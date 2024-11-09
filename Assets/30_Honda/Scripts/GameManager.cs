using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject m_player = null;
    public GameObject m_enemyPrefab = null;
    float m_elapsedTime = 0.0f; // 経過時間
    bool m_counterFg = false;   // 時間計測フラグ


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // エンターキーが押された時、計測の切り替え
        if(Input.GetKeyDown(KeyCode.Return))
        {
            m_counterFg = !m_counterFg; // 反対の値に変更
        }

        // 時間を計測
        if(m_counterFg == true)
        {
            m_elapsedTime += Time.deltaTime;    // 経過時間の加算
            Debug.Log("計測中:" + (m_elapsedTime).ToString());
        }

        if(m_elapsedTime % 100.0f == 0)
        {
            CreateObject(m_enemyPrefab);
        }
    }

    // オブジェクトを生成する
    private void CreateObject(GameObject _gameObject)
    {
        GameObject _newObject;
        _newObject = Instantiate(_gameObject, this.transform.position, Quaternion.identity);
    }
}
