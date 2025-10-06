using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasAnalyzerController : MonoBehaviour
{
    [SerializeField] private Image _displayBackground;
    [SerializeField] private Text _distanceText;

    private float _activationTime = 3.0f;
    private float _activationProgress = 0f;
    private bool _isPoweredOn = false;
    private float _buttonHoldTime = 0f;
    private bool _isButtonHeld = false;
    private Color _displayColor;

    [SerializeField] private Transform _probe;
    [SerializeField] private string _dangerTag = "DangerZone";
    private GameObject[] _dangerZones;

    private void Start()
    {
        _displayColor = _displayBackground.color;
        _displayBackground.color = Color.clear;
        _distanceText.gameObject.SetActive(false);
        _dangerZones = GameObject.FindGameObjectsWithTag(_dangerTag);
    }

    private void Update()
    {
        if (_isButtonHeld) HandleButtonHold();
        if (_isPoweredOn) UpdateDistanceText();
    }

    public void OnButtonPressed()
    {
        _isButtonHeld = true;
        _buttonHoldTime = 0f;
    }

    public void OnButtonReleased()
    {
        _isButtonHeld = false;
        SetDisplayState(_isPoweredOn);
    }

    private void HandleButtonHold()
    {
        _buttonHoldTime += Time.deltaTime;
        _activationProgress = _buttonHoldTime / _activationTime;

        if (!_isPoweredOn) UpdateProgressIndication(_activationProgress);

        if (_buttonHoldTime >= _activationTime)
        {
            TogglePower();
            _isButtonHeld = false;
        }
    }

    private void TogglePower()
    {
        _isPoweredOn = !_isPoweredOn;
        SetDisplayState(_isPoweredOn);
    }

    private void SetDisplayState(bool isOn)
    {
        _distanceText.gameObject.SetActive(isOn);
        _displayBackground.color = isOn ? _displayColor : Color.clear;
    }

    private void UpdateProgressIndication(float progress)
    {
        Color progressColor = Color.Lerp(Color.clear, _displayColor, progress);
        _displayBackground.color = progressColor;
    }

    private void UpdateDistanceText()
    {
        float minDistance = float.MaxValue;
        for (int i = 0; i < _dangerZones.Length; i++)
        {
            float distance = Vector3.Distance(_probe.position, _dangerZones[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
            }
        }
        _distanceText.text = $"{minDistance:F2}m";
    }
}
