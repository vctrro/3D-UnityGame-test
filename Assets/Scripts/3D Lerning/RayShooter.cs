﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] public AudioSource shootSound;
    private Camera _camera;
    private ReactiveTarget _target;
    private GameObject _targetObject;
    private float _hitForce = 3.0f;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shootSound.Play();
            Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _targetObject = hit.transform.gameObject;
                _target = _targetObject.GetComponent<ReactiveTarget>();
                if (_target != null)
                {
                    Debug.Log($"Target hit in {hit.point}");
                    _target.ReactToHit();
                    hit.rigidbody.AddForce(ray.direction * _hitForce);
                }
                StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

    private void OnGUI() {
        int size = 12;
        float posX = _camera.pixelWidth/2 - size/4;
        float posY = _camera.pixelHeight/2 -size/2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Renderer rend = sphere.GetComponent<Renderer>();
        rend.material = Resources.Load<Material>("fire");
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForEndOfFrame();
            sphere.transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
        }
        Destroy(sphere);
    }
}
