using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using UnityEngine;

namespace Assets.Scripts.Database
{
    public class LocalDatabase : MonoBehaviour
    {
        // singleton for easier access
        public static LocalDatabase singleton;

        // file name
        public string databaseFile = "Database.sqlite";

        // connection
        SQLiteConnection connection;

        class accounts
        {
            [PrimaryKey] // important for performance: O(log n) instead of O(n)
            public string name { get; set; }
            public string password { get; set; }
            // created & lastlogin for statistics like CCU/MAU/registrations/...
            public DateTime created { get; set; }
            public DateTime lastlogin { get; set; }
            public bool banned { get; set; }

        }
        void Awake()
        {
            // initialize singleton
            if (singleton == null) singleton = this;
        }
        class characters
        {
            [PrimaryKey] // important for performance: O(log n) instead of O(n)
            [Collation("NOCASE")] // [COLLATE NOCASE for case insensitive compare. this way we can't both create 'Archer' and 'archer' as characters]
            public string name { get; set; }
            [Indexed] // add index on account to avoid full scans when loading characters
            public string account { get; set; }
        }
        class character_inventory
        {
            public string character { get; set; }
            public int slot { get; set; }
            public string name { get; set; }
            public int amount { get; set; }
        }
            public void Connect()
        {
            // database path: Application.dataPath is always relative to the project,
            // but we don't want it inside the Assets folder in the Editor (git etc.),
            // instead we put it above that.
            // we also use Path.Combine for platform independent paths
            // and we need persistentDataPath on android
#if UNITY_EDITOR
            string path = Path.Combine(Directory.GetParent(Application.dataPath).FullName, databaseFile);
#elif UNITY_ANDROID
        string path = Path.Combine(Application.persistentDataPath, databaseFile);
#elif UNITY_IOS
        string path = Path.Combine(Application.persistentDataPath, databaseFile);
#else
        string path = Path.Combine(Application.dataPath, databaseFile);
#endif

            // open connection
            // note: automatically creates database file if not created yet
            connection = new SQLiteConnection(path);


            connection.CreateTable<accounts>();
            connection.CreateTable<characters>();
            connection.CreateTable<character_inventory>();
        }
        class character_quests
        {
            public string character { get; set; }
            public string name { get; set; }
            public int progress { get; set; }
            public bool completed { get; set; }
            // PRIMARY KEY (character, name) is created manually.
        }
        class character_actions
        {
            public string character { get; set; }
            public string name { get; set; }
            public int level { get; set; }
            public float castTimeEnd { get; set; }
            public float cooldownEnd { get; set; }
            // PRIMARY KEY (character, name) is created manually.
        }

        // close connection when Unity closes to prevent locking
        void OnApplicationQuit()
        {
            connection?.Close();
        }

    }
}