using System;
using System.Collections.Generic;
using System.Linq;

namespace Elephant.ActivityTrackers
{
	/// <summary>
	/// Tracker for different activities using generics, counting the number
	/// of times each activity occurs.
	/// </summary>
	public class ActivityTrackerGeneric<TActivityKey, TActivityData>
		where TActivityData : new()
	{
		/// <summary>
		/// Data container, used for keeping track of both the <see cref="Count"/>
		/// and the activities.
		/// </summary>
		private sealed class Data
		{
			/// <summary>
			/// Amount of activities that are currently being processed.
			/// </summary>
			internal int Count { get; set; }

			/// <summary>
			/// Activity data list that is being processed that may contain custom user data.
			/// </summary>
			internal List<TActivityData> ActivityDataList { get; set; } = new();
		}

		/// <summary>
		/// Activities currently being processed.
		/// </summary>
		private readonly Dictionary<TActivityKey, Data> _activities = new();

		/// <summary>
		/// Returns true if there is at least 1 activity present.
		/// </summary>
		public bool HasAny => _activities.Any();

		/// <summary>
		/// Returns true if there are no activities present at all.
		/// </summary>
		public bool IsEmpty => !_activities.Any();

		/// <summary>
		/// Returns total count of all activity keys.
		/// This does not include multiple activities being processed more than once.
		/// </summary>
		public int TotalActivityCount => _activities.Count;

		/// <summary>
		/// Returns specified activity (<paramref name="activityKey"/>) count.
		/// </summary>
		/// <param name="activityKey">The name of the activity.</param>
		/// <returns><paramref name="activityKey"/> count.</returns>
		public int ActivityCount(TActivityKey activityKey)
		{
			return _activities.TryGetValue(activityKey, out Data activity) ? activity.Count : 0;
		}

		/// <summary>
		/// Adds a count to the specified activity.
		/// </summary>
		/// <param name="activityKey">Activity name to add a count to.</param>
		/// <param name="data">Custom user data.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="activityKey"/> is null.</exception>
		public void Add(TActivityKey activityKey, TActivityData data)
		{
			if (activityKey == null)
				throw new ArgumentNullException(nameof(activityKey));

			if (_activities.ContainsKey(activityKey))
			{
				Data activityInfo = _activities[activityKey];
				activityInfo.Count++;
				activityInfo.ActivityDataList.Add(data);
				_activities[activityKey] = activityInfo;
			}
			else
			{
				_activities.Add(activityKey, new Data { Count = 1, ActivityDataList = new() { data } });
			}
		}

		/// <summary>
		/// Returns data that belongs to <paramref name="activityKey"/> or an empty list
		/// if currently no such <paramref name="activityKey"/> exists.
		/// </summary>
		/// <param name="activityKey">Activity name.</param>
		public List<TActivityData> GetData(TActivityKey activityKey)
		{
			if (_activities.TryGetValue(activityKey, out Data resultData))
				return resultData.ActivityDataList;

			return new();
		}

		/// <summary>
		/// Returns true if the specified activity (<paramref name="activityKey"/>)
		/// is being processed.
		/// </summary>
		/// <param name="activityKey">Activity name to check.</param>
		/// <returns>Returns true if the activity is being processed; otherwise, false.</returns>
		public bool IsBeingProcessed(TActivityKey activityKey)
		{
			return _activities.ContainsKey(activityKey) && _activities[activityKey].Count > 0;
		}

		/// <summary>
		/// Decreases specified activity (<paramref name="activityKey"/>) count by 1 or removes
		/// it if its count reaches zero, regardless of activity data.
		/// </summary>
		/// <param name="activityKey">The name of the activity to decrease the count of.</param>
		public void Remove(TActivityKey activityKey)
		{
			if (_activities.ContainsKey(activityKey))
			{
				if (_activities[activityKey].Count > 1)
					_activities[activityKey].Count--;
				else
					_ = _activities.Remove(activityKey);
			}
		}

		/// <summary>
		/// Decreases specified activity (<paramref name="activityKey"/>) with the specified
		/// <paramref name="activityData"/> count by 1 or removes it if its count reaches zero.
		/// </summary>
		/// <param name="activityKey">The name of the activity to decrease the count of.</param>
		/// <param name="activityData">The activity data to be specifically removed.</param>
		/// <returns>true if removed; otherwise, false.</returns>
		public bool Remove(TActivityKey activityKey, TActivityData activityData)
		{
			if (_activities.ContainsKey(activityKey) && _activities[activityKey].ActivityDataList.Remove(activityData))
			{
				_activities[activityKey].Count--;
				return true;
			}

			return false;
		}

		/// <summary>
		/// Removes all activities from the tracker.
		/// </summary>
		public void RemoveAll()
		{
			_activities.Clear();
		}
	}
}
