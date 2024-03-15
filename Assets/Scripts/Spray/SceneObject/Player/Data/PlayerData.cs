using BF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Spray
{
	[Serializable]
	public class DefaultPlayerData
	{
        public bool isAlive;
        public int mass;
		public bool isUnmatched;
	}
	public class PlayerData : BaseShareData
	{
		public DataWithEvent<int> mass;
        public DataWithEvent<bool> isUnmatched;

        public Transform model;

        [SerializeField] DefaultPlayerData defaultValue;
		
        new public void Awake()
		{
			mass = new DataWithEvent<int>();
			isUnmatched = new DataWithEvent<bool>();
			isAlive = new DataWithEvent<bool>();
			mass.Value = defaultValue.mass;
			isAlive.Value = defaultValue.isAlive;
			isUnmatched.Value = defaultValue.isUnmatched;
		}
    }
}