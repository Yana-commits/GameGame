using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private List<Vector3> spawnPositions;

    private int pointNomber;
    void Start()
    {
        
    }

    public static Field Create(LevelParameters level, LevelRepository levelRepository, Enemy enemy,Player player)
    {
        var h_size = level.H_size;
        var w_size = level.W_size;
        Vector3 fieldPosition = Vector3.zero;
        var field = Instantiate(Resources.Load("Prefabs/Field") as GameObject, fieldPosition, Quaternion.identity);
        Vector3 scale = Vector3.one;

        scale.x = h_size;
        scale.y = 0.1f;
        scale.z = w_size;
        field.transform.localScale = scale;

        Vector3 cameraPosition = field.transform.position;

        cameraPosition.x = 6;
        cameraPosition.y = 14;

        Camera.main.transform.position = cameraPosition;
        Camera.main.orthographicSize = (float)h_size * 0.6f;

        field.gameObject.GetComponent<Field>().CreateEnemie(level, enemy, levelRepository);
        field.gameObject.GetComponent<Field>().CreateChips(level, levelRepository);
        field.gameObject.GetComponent<Field>().CreatePlayer(level, player);

        return field.gameObject.GetComponent<Field>();
    }
    private void CreateChips(LevelParameters level, LevelRepository levelRepository)
    {
         var h_size = level.H_size;
        var w_size = level.W_size;
        int sidePosition;

        if (((w_size - 1) / 3) % 2 == 0)
        {
            pointNomber = (((h_size - 1) / 5) * ((w_size - 1) / 3))/2;
            sidePosition = (w_size - 5) / 2;

            VerticalPoints(level, levelRepository, sidePosition);
            VerticalPoints(level, levelRepository, -sidePosition);
        }
        else
        {
            pointNomber = ((h_size - 1) / 5) * ((w_size - 1) / 3);
            sidePosition = 0;
            VerticalPoints(level, levelRepository, sidePosition);
        }
    }
    private void VerticalPoints(LevelParameters level, LevelRepository levelRepository, int sidePosition)
    {
        var h_size = level.H_size;
        var spawnPositions = new List<Vector3>();
        var chipfield = levelRepository.LevelList[Controller.Instance.Index].platforms;
        Vector3 previousPosition = new Vector3(-((h_size - 6) / 2), 0.1f, sidePosition);

        spawnPositions.Add(previousPosition);

        for (int i = 0; i < pointNomber - 1; i++)
        {
            previousPosition = previousPosition + new Vector3(6, 0, 0);

            spawnPositions.Add(previousPosition);
        }
        for (int i = 0; i < pointNomber; i++)
        {
            int index = Random.Range(0, chipfield.Length);
            ChipField newChipField = Instantiate(chipfield[index], spawnPositions[i], Quaternion.identity) as ChipField;
        }
    }
    private void CreateEnemie(LevelParameters level, Enemy enemy, LevelRepository levelRepository)
    {
        var h_size = level.H_size;
        var w_size = level.W_size;
        var nomber = levelRepository.LevelList[Controller.Instance.Index].enemyNomber * level.IncreaseNomber;
        for (int i = 0; i < nomber; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-(h_size-1) / 2, (h_size-1) / 2), 0.3f, Random.Range(-(w_size-1) / 2, (w_size-1) / 2));
            Enemy newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity) as Enemy;
        } 
    }
    private void CreatePlayer(LevelParameters level, Player player)
    {
        var h_size = level.H_size;
        var w_size = level.W_size;
        Vector3 spawnPosition = new Vector3(-h_size / 2, 0.3f, -w_size / 2);
      
        Player newPlayer = Instantiate(player, spawnPosition, Quaternion.identity) as Player;
    }
}
