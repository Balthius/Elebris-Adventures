using UnityEngine;

namespace Assets.Scripts.Units
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : Entity
    {
        [Header("Movement")]
        [Range(0, 1)] public float moveProbability = 0.1f; // chance per second
        public float moveDistance = 10;
        // monsters should follow their targets even if they run out of the movement
        // radius. the follow dist should always be bigger than the biggest archer's
        // attack range, so that archers will always pull aggro, even when attacking
        // from far away.
        public float followDistance = 20;
        [Range(0.1f, 1)] public float attackToMoveRangeRatio = 0.8f; // move as close as 0.8 * attackRange to a target

        [Header("Experience Reward")]
        public long rewardExperience = 10;
        public long rewardSkillExperience = 2;

        [Header("Loot")]
        public int lootGoldMin = 0;
        public int lootGoldMax = 10;
        public ItemDropChance[] dropChances; 
        //TODO: add a line this to accept arrays of arrays, for standardized loot-systems based on other factors such as region, race, faction  
        public ParticleSystem lootIndicator;
        // note: Items have a .valid property that can be used to 'delete' an item.
        //       it's better than .RemoveAt() because we won't run into index-out-of
        //       range issues

        [Header("Respawn")]
        public float deathTime = 30f; // enough for animation & looting
        double deathTimeEnd; // double for long term precision
        public bool respawn = true;
        public float respawnTime = 10f;
        double respawnTimeEnd; // double for long term precision


        // save the start position for random movement distance and respawning
        Vector3 startPosition;

        // the last skill that was casted, to decide which one to cast next
        int lastSkill = -1;

        protected override void Start()
        {
            base.Start();

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        protected override void Update()
        {
            base.Update();
        }
    }

}