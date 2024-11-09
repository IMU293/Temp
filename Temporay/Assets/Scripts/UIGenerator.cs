using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGenerator : MonoBehaviour
{
    public Canvas canvas;
    public GameObject textPrefab;
    private TextMeshProUGUI timeText;
    private float elapsedTime = 0f;

    public GameObject crosshairPrefab; // 조준점 UI 프리팹을 참조
    private GameObject crosshair;

    private GameObject deadMasage;
    public GameObject deadPrefab;

    void Start()
    {
        CreateText(new Vector2(0, -50), "경과 시간: 00:00");

        // 화면 중앙에 조준점을 생성
        crosshair = Instantiate(crosshairPrefab, canvas.transform);
        crosshair.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, -10);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        float seconds = elapsedTime % 60;
        timeText.text = string.Format("{0:D2}:{1:D2}", minutes, Mathf.FloorToInt(seconds));
    }

    void CreateText(Vector2 position, string initialText)
    {
        GameObject textObj = Instantiate(textPrefab, canvas.transform);

        // RectTransform을 가져와서 위치를 화면 상단 중앙에 설정
        RectTransform rectTransform = textObj.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = position;  // (0, 100)으로 시작 위치 설정

        // Anchor 설정 (상단 중앙)
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 1f);

        // 텍스트 내용 설정
        timeText = textObj.GetComponent<TextMeshProUGUI>();
        timeText.text = initialText;
    }
}

