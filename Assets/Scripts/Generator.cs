﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public GameObject roadChunk;
    public GameObject roadChunk2;
    public GameObject roadChunk3;
    public GameObject roadChunk4;





    // distance between edges of the chunk.
    public float chunkLength;

    // number of chunks to be activated at a time.
    public int drawingAmount = 3;

    // reference to player object. it is required to manage chunks on the scene.
    [SerializeField] private Transform player = null;

    // total number of chunks that actually exist in the scene
    [SerializeField] private int numberOfChunks = 7;

    // list of references to chunks in the scence
    private Queue<Transform> chunks;

    // reference to chunk that the player is on
    private Transform currentChunk;
    private int indexOfCurrentChunk;
    private int currentChunkPosition = 0;
    private int WhichChunk = 0;
    private Random rnd = new Random();
    private void Awake()
    {
        InitializeChunksList();
    }

    private void InitializeChunksList()
    {
        chunks = new Queue<Transform>();
        for (int i = 0; i < numberOfChunks; i++)
        {

      
            switch (WhichChunk)
            {
                case 0:
                    {
                        GameObject _chunk = Instantiate<GameObject>(roadChunk);
                        if (i != 0)
                            _chunk.SetActive(false);
                        chunks.Enqueue(_chunk.transform);
                        WhichChunk = Random.Range(0,4);
                        break;
                    }
                case 1:
                    {
                        GameObject _chunk = Instantiate<GameObject>(roadChunk2);
                        _chunk.transform.position = NextChunkPosition();
                        if (i != 0)
                            _chunk.SetActive(false);
                        chunks.Enqueue(_chunk.transform);
                        WhichChunk = Random.Range(0, 4);
                        break;
                    }
                case 2:
                    {
                        GameObject _chunk = Instantiate<GameObject>(roadChunk3);
                        _chunk.transform.position = NextChunkPosition();
                        if (i != 0)
                            _chunk.SetActive(false);
                        chunks.Enqueue(_chunk.transform);
                        WhichChunk = Random.Range(0, 4);
                        break;
                    }
                case 3:
                    {
                        GameObject _chunk = Instantiate<GameObject>(roadChunk4);
                        _chunk.transform.position = NextChunkPosition();
                        if (i != 0)
                            _chunk.SetActive(false);
                        chunks.Enqueue(_chunk.transform);
                        WhichChunk = Random.Range(0, 4);
                        break;
                    }
                default:
                    break;
            }
        }
    }

    private void Start()
    {
        //Vector3 nextPos = Plane.transformn.positon + (Plane.transform.forward * (Plane.transform.localScale.x * 10 ))
    }

    private void FixedUpdate()
    {

        if (!player) return;

        // determine the chunk that the player is on
        currentChunk = GetCurrentChunk();
        indexOfCurrentChunk = GetIndexOfCurrentChunk();

        // Manage chunks based on current chunk that the player is on
        for (int i = indexOfCurrentChunk; i < (indexOfCurrentChunk + drawingAmount); i++)
        {
            i = Mathf.Clamp(i, 0, chunks.Count - 1);
            GameObject _chunkGO = (chunks.ToArray()[i]).gameObject;
            if (!_chunkGO.activeInHierarchy)
                _chunkGO.SetActive(true);
        }

        if (indexOfCurrentChunk > 0)
        {
            float _distance = Vector3.Distance(player.position, (chunks.ToArray()[indexOfCurrentChunk - 1]).position);
            if (_distance > (chunkLength * .75f))
                SweepPreviousChunk();
        }

    }

    private void SweepPreviousChunk()
    {
        Transform _chunk = chunks.Dequeue();
        _chunk.gameObject.SetActive(false);
        _chunk.position = NextChunkPosition();
        chunks.Enqueue(_chunk);
    }

    private Vector3 NextChunkPosition()
    {
        float _position = currentChunkPosition;
        currentChunkPosition += (int)chunkLength;
        return new Vector3(_position, 0, 0);
    }

    private Transform GetCurrentChunk()
    {
        Transform current_chunk = null;
        foreach (Transform c in chunks)
        {
            if (Vector3.Distance(player.position, c.position) <= (chunkLength / 2))
            {
                current_chunk = c;
                break;
            }
        }
        return current_chunk;
    }

    private int GetIndexOfCurrentChunk()
    {
        int index = -1;
        for (int i = 0; i < chunks.Count; i++)
        {
            if ((chunks.ToArray()[i]).Equals(currentChunk))
            {
                index = i;
                break;
            }
        }
        return index;
    }

}
