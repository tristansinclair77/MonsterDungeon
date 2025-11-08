using System;
using System.Collections.Generic;
using MonsterDungeon.Domain.Entities;
using MonsterDungeon.Domain.Enums;

namespace MonsterDungeon.Domain.Services
{
    /// <summary>
    /// Factory for creating enemies and obstacles based on GDD rules
    /// Based on GDD Section 3: Enemy and Boss Spawning
    /// </summary>
    public class EnemyFactory
    {
        private readonly Random _random;

        public EnemyFactory()
        {
            _random = new Random();
        }

        /// <summary>
        /// Spawn an enemy based on difficulty level and theme
        /// </summary>
        public Enemy SpawnEnemy(int difficultyLevel, DungeonTheme theme)
        {
            EnemyType enemyType = DetermineEnemyType(difficultyLevel, theme);

            var enemy = new Enemy
            {
                Type = enemyType,
                Health = CalculateHealth(enemyType, difficultyLevel),
                Attack = CalculateAttack(enemyType, difficultyLevel),
                Defense = CalculateDefense(enemyType, difficultyLevel),
                IsBlock = false
            };

            return enemy;
        }

        /// <summary>
        /// Spawn a block (obstacle)
        /// </summary>
        public Enemy SpawnBlock(int difficultyLevel)
        {
            var block = new Enemy
            {
                Type = EnemyType.Block,
                Health = CalculateBlockHealth(difficultyLevel),
                Attack = 0,
                Defense = 999,
                IsBlock = true
            };

            return block;
        }

        /// <summary>
        /// Spawn a boss enemy
        /// </summary>
        public Enemy SpawnBoss(int level, DungeonTheme theme)
        {
            var boss = new Enemy
            {
                Type = EnemyType.Boss,
                Health = 100 + (level * 20),
                Attack = 15 + (level * 3),
                Defense = 10 + (level * 2),
                IsBoss = true
            };

            return boss;
        }

        private EnemyType DetermineEnemyType(int difficulty, DungeonTheme theme)
        {
            // Basic enemy type determination based on difficulty
            // TODO: Expand based on GDD enemy tables

            var enemyTypes = new List<EnemyType>
            {
                EnemyType.Goblin,
                EnemyType.Skeleton,
                EnemyType.Orc,
                EnemyType.Wraith
            };

            int index = Math.Min(difficulty / 3, enemyTypes.Count - 1);
            return enemyTypes[index];
        }

        private int CalculateHealth(EnemyType type, int difficulty)
        {
            int baseHealth;
            switch (type)
            {
                case EnemyType.Goblin:
                    baseHealth = 10;
                    break;
                case EnemyType.Skeleton:
                    baseHealth = 15;
                    break;
                case EnemyType.Orc:
                    baseHealth = 25;
                    break;
                case EnemyType.Wraith:
                    baseHealth = 20;
                    break;
                case EnemyType.Block:
                    baseHealth = 50;
                    break;
                default:
                    baseHealth = 10;
                    break;
            }

            return baseHealth + (difficulty * 2);
        }

        private int CalculateAttack(EnemyType type, int difficulty)
        {
            int baseAttack;
            switch (type)
            {
                case EnemyType.Goblin:
                    baseAttack = 3;
                    break;
                case EnemyType.Skeleton:
                    baseAttack = 5;
                    break;
                case EnemyType.Orc:
                    baseAttack = 8;
                    break;
                case EnemyType.Wraith:
                    baseAttack = 7;
                    break;
                default:
                    baseAttack = 3;
                    break;
            }

            return baseAttack + (difficulty / 2);
        }

        private int CalculateDefense(EnemyType type, int difficulty)
        {
            int baseDefense;
            switch (type)
            {
                case EnemyType.Goblin:
                    baseDefense = 1;
                    break;
                case EnemyType.Skeleton:
                    baseDefense = 3;
                    break;
                case EnemyType.Orc:
                    baseDefense = 5;
                    break;
                case EnemyType.Wraith:
                    baseDefense = 2;
                    break;
                default:
                    baseDefense = 1;
                    break;
            }

            return baseDefense + (difficulty / 3);
        }

        private int CalculateBlockHealth(int difficulty)
        {
            return 50 + (difficulty * 5);
        }

        /// <summary>
        /// Determine spawn rate based on difficulty
        /// </summary>
        public double GetSpawnRate(int difficulty)
        {
            // Spawn rate increases with difficulty
            return 0.3 + (difficulty * 0.05);
        }
    }
}
