using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;


namespace App.Helpers
{
	public class DataManager : MonoBehaviour
	{
		public int Version { get; set; }

		private const string Directory = "gData";
		private const string File = "ht3ds.hl";
		private static string Path => System.IO.Path.Combine(Application.persistentDataPath, Directory);
		private static string FullPath => $"{Path}/{File}";

		public static DataManager Instance { get; private set; }

		public UserSave UserData { get; private set; }

		private enum StateData
		{
			Error,
			Exist,
			Empty
		}

		public bool IsCompleted { get; set; }

		private const int MaxTryAmount = 3;
		private int _tryCounter;


#if UNITY_EDITOR
		private void OnValidate()
		{
			Version = PlayerSettings.Android.bundleVersionCode;
#if UNITY_IOS
			Version = Convert.ToInt16(PlayerSettings.iOS.buildNumber);
#endif
		}
#endif

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
				Setup();
			}
			else
			{
				Destroy(this);
			}
		}

		public void DeleteDataBase()
		{
			if (System.IO.File.Exists(FullPath))
				System.IO.File.Delete(FullPath);

			PlayerPrefs.DeleteAll();

			Debug.Log("Delete Success");
		}

		#region General Database Functions

		private void Setup()
		{
			var result = CheckData();
			var success = false;

			switch (result)
			{
				case StateData.Error:
					Debug.LogError("DataManager => CheckData");
					break;
				case StateData.Exist:
					success = Load();
					break;
				case StateData.Empty:
					success = Create();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (!success)
			{
				if (_tryCounter < MaxTryAmount)
				{
					_tryCounter++;
					DeleteDataBase();
					Setup();
					return;
				}
				else
				{
					Debug.LogError("We tried so much. You don't deserve play this game! :(");
				}
			}

			IsCompleted = true;
		}

		private StateData CheckData()
		{
			try
			{
				return System.IO.File.Exists(FullPath) ? StateData.Exist : StateData.Empty;
			}
			catch
			{
				return StateData.Error;
			}
		}

		private bool Create()
		{
			if (!System.IO.Directory.Exists(Path))
				System.IO.Directory.CreateDirectory(Path);

			UserData = new UserSave(Version);

			return Save();
		}

		private bool Load()
		{
			var bf = new BinaryFormatter();
			var file = System.IO.File.Open(FullPath, FileMode.Open);

			if (file.Length == 0)
			{
				file.Close();
				return false;
			}

			try
			{
				UserData = (UserSave)bf.Deserialize(file);
				file.Close();
				return UpdateDatabase();
			}
			catch (Exception exception)
			{
				Debug.LogError("DataManager => Load Error");
				Debug.LogError(exception);

				file.Close();
				return false;
			}
		}

		public bool Save()
		{
			if (UserData == null)
			{
				Debug.LogError("DataManager => Save => UserData==NULL");
				return false;
			}

			try
			{
				var bf = new BinaryFormatter();
				var file = System.IO.File.Create(FullPath);

				bf.Serialize(file, UserData);
				file.Close();

				return true;
			}
			catch (Exception exception)
			{
				Debug.LogError("DataManager => Save Error");
				Debug.LogError(exception);

				return false;
			}
		}

		private bool UpdateDatabase()
		{
			if (UserData.Version == Version)
				return true;

			//EXAMPLE
			// if (UserData.Version < X)
			// {
			// 	//DO Something
			// }

			//Set upgrade
			UserData.Version = Version;

			return Save();
		}

		#endregion
	}


	[Serializable]
	public class UserSave
	{
		public int Version { get; set; }

		public UserSave(int version)
		{
			Debug.Log("User Created");
			this.Version = version;
		}
	}
}