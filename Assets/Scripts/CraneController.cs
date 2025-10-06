using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, East, West, North, South }

public class CraneController : MonoBehaviour
{
    [SerializeField] private Transform _hook;
    [SerializeField] private float _hookMinY;
    [SerializeField] private float _hookMaxY;

    [SerializeField] private Transform _crane;
    [SerializeField] private float _craneMinX;
    [SerializeField] private float _craneMaxX;

    [SerializeField] private Transform _beam;
    [SerializeField] private float _beamMinZ;
    [SerializeField] private float _beamMaxZ;

    [SerializeField] private Transform _reel;
    [SerializeField] private AudioSource _reelAudioSource;
    [SerializeField] private AudioClip _reelAudio;
    [SerializeField] private Transform _wire;

    private float _reelRotationSpeed = 180f;
    private bool _isAudioPlaying = false;
    private Vector3 _initialWireScale;
    private float _initialWireDistance;

    private float _upSpeed = 1.0f;
    private float _downSpeed = 1.5f;
    private float _eastSpeed = 1.1f;
    private float _westSpeed = 1.2f;
    private float _northSpeed = 1.3f;
    private float _southSpeed = 1.4f;

    private bool _isMoving;
    private Direction _direction;

    private void OnEnable()
    {
        CraneEvents.OnMoveUp += MoveUp;
        CraneEvents.OnMoveDown += MoveDown;
        CraneEvents.OnMoveEast += MoveEast;
        CraneEvents.OnMoveWest += MoveWest;
        CraneEvents.OnMoveNorth += MoveNorth;
        CraneEvents.OnMoveSouth += MoveSouth;
        CraneEvents.OnStopMoving += StopMoving;
    }

    private void OnDisable()
    {
        CraneEvents.OnMoveUp -= MoveUp;
        CraneEvents.OnMoveDown -= MoveDown;
        CraneEvents.OnMoveEast -= MoveEast;
        CraneEvents.OnMoveWest -= MoveWest;
        CraneEvents.OnMoveNorth -= MoveNorth;
        CraneEvents.OnMoveSouth -= MoveSouth;
        CraneEvents.OnStopMoving -= StopMoving;
    }

    private void Start()
    {
        _initialWireScale = _wire.localScale;
        _initialWireDistance = _reel.position.y - _hook.position.y;
        _reelAudioSource.clip = _reelAudio;
    }

    private void MoveUp()
    {
        _isMoving = true;
        _direction = Direction.Up;
    }
    private void MoveDown()
    {
        _isMoving = true;
        _direction = Direction.Down;
    }
    private void MoveEast()
    {
        _isMoving = true;
        _direction = Direction.East;
    }
    private void MoveWest()
    {
        _isMoving = true;
        _direction = Direction.West;
    }
    private void MoveNorth()
    {
        _isMoving = true;
        _direction = Direction.North;
    }
    private void MoveSouth()
    {
        _isMoving = true;
        _direction = Direction.South;
    }
    private void StopMoving()
    {
        _isMoving = false;
    }

    private void Update()
    {
        UpdateAudio();

        if (!_isMoving) return;

        switch (_direction)
        {
            case Direction.Up:
                if (_hook.localPosition.y >= _hookMaxY) break;
                _hook.localPosition += _upSpeed * Time.deltaTime * _hook.up;
                UpdateReel(_upSpeed);
                break;
            case Direction.Down:
                if (_hook.localPosition.y <= _hookMinY) break;
                _hook.localPosition -= _downSpeed * Time.deltaTime * _hook.up;
                UpdateReel(-_downSpeed);
                break;
            case Direction.East:
                if (_crane.localPosition.x <= _craneMinX) break;
                _crane.localPosition -= _eastSpeed * Time.deltaTime * _crane.right;
                break;
            case Direction.West:
                if(_crane.localPosition.x >= _craneMaxX) break;
                _crane.localPosition += _westSpeed * Time.deltaTime * _crane.right;
                break;
            case Direction.North:
                if (_beam.localPosition.z <= _beamMinZ) break;
                _beam.localPosition -= _northSpeed * Time.deltaTime * _beam.forward;
                break;
            case Direction.South:
                if (_beam.localPosition.z >= _beamMaxZ) break;
                _beam.localPosition += _southSpeed * Time.deltaTime * _beam.forward;
                break;
            default:
                break;
        }
    }

    private void UpdateAudio()
    {
        if (!_isAudioPlaying && _isMoving && (_direction == Direction.Up || _direction == Direction.Down))
        {
            _reelAudioSource.Play();
            _isAudioPlaying = true;
        }

        if (_isAudioPlaying && (!_isMoving || _hook.localPosition.y >= _hookMaxY || _hook.localPosition.y <= _hookMinY))
        {
            _reelAudioSource.Stop();
            _isAudioPlaying = false;
        }
    }

    private void UpdateReel(float speed)
    {
        float rotationAmount = speed * _reelRotationSpeed * Time.deltaTime;
        _reel.Rotate(rotationAmount, 0, 0, Space.Self);

        float distance = _reel.position.y - _hook.position.y;

        _wire.position = new(
            _wire.position.x, 
            (_reel.position.y + _hook.position.y) * 0.5f,
            _wire.position.z
            );

        float scaleMultiplier = distance / _initialWireDistance;
        _wire.localScale = new(
            _initialWireScale.x,
            _initialWireScale.y * scaleMultiplier,
            _initialWireScale.z
            );
    }
}
