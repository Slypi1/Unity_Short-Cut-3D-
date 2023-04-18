using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path: MonoBehaviour
{
    [SerializeField] private Line _line;
    private List<Transform> _slots = new List<Transform>();
    private Transform _slotStart;
 
    #region Event

    private void OnEnable()
    {            
        InputSystem.OnStartSlot += StartPath;
        InputSystem.OnStartFindPath += StartFindPath;
    }

    private void OnDisable()
    {      
        InputSystem.OnStartSlot -= StartPath;
        InputSystem.OnStartFindPath -= StartFindPath;
    }
    #endregion

    public void AddSlot(Transform slot) => _slots.Add(slot);
    
    private void StartPath(Transform slot) => _slotStart = slot;
 
    private void StartFindPath(Transform slotEnd)
    {
        List <Transform> pathShort = FindShortestPath(_slotStart,slotEnd);       
        FoundPath(pathShort);
    }
    private void FoundPath(List <Transform> path)
    {
        if (path != null)      
            _line.GetPath(path);    
        else 
            Debug.Log("No path found");
    }

    private List<Transform> FindShortestPath(Transform startSlot,Transform endSlot)
    {
        var visited = new HashSet<Transform>();

        var queue = new Queue<List<Transform>>();
        queue.Enqueue(new List<Transform> { startSlot });

        while (queue.Count > 0)
        {
            var path = queue.Dequeue();
            var lastSlot = path.Last();

            if (visited.Contains(lastSlot))
                continue;

            visited.Add(lastSlot);

            if (lastSlot.position == endSlot.position)
                return path;

            List<Transform> connec = AddNeigboringSlot(lastSlot);

            foreach (Transform neighbor in connec)
            {
                if (visited.Contains(neighbor))
                    continue;

                List<Transform> newPath = new List<Transform>(path);
                newPath.Add(neighbor);
                queue.Enqueue(newPath);

            }
        }
        
        return null;
    }
     
    /// <summary>
    /// Adjacent slot search
    /// </summary>
    /// <param name="last"></param>
    /// <returns></returns>
    private List<Transform> AddNeigboringSlot(Transform last)
    {
        List <Transform> connections = new List<Transform>();
        foreach(Transform slot in _slots)
        {
            if (Math.Abs(last.position.x - slot.position.x) +
            Math.Abs(last.position.z - slot.position.z) == 1 &&
            Math.Abs(last.localScale.y - slot.localScale.y) <= 1 &&
            last.position != slot.position)
            {
                connections.Add(slot);
            }
        }
        return connections;
    }
}
