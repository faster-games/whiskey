using System;
using System.Collections.Generic;
using FasterGames.Whiskey.Boxes;
using UnityEngine;

namespace FasterGames.Whiskey.Tables
{
    /// <summary>
    /// A weighted probability table for values of type {T}.
    /// </summary>
    /// <typeparam name="T">value type</typeparam>
    public abstract class Table<T> : ScriptableObject, IReadable<T>
    {
        /// <summary>
        /// Represents an entry in the probability table
        /// </summary>
        [Serializable]
        public class TableEntry
        {
            /// <summary>
            /// The weight of the object
            /// </summary>
            public float weight = 1f;

            /// <summary>
            /// The value of the object
            /// </summary>
            public Ref<T> value;

            /// <summary>
            /// Editor preview of the object change
            /// </summary>
            internal float Chance;

            /// <summary>
            /// The entry low bound
            /// </summary>
            internal float LowBound { get; set; }

            /// <summary>
            /// The entry high bound
            /// </summary>
            internal float HighBound { get; set; }
        }

        /// <summary>
        /// The rng source for the table querying operations
        /// </summary>
        [SerializeField]
        public RngSource random;
        
        /// <summary>
        /// The object entries
        /// </summary>
        [SerializeField]
        protected List<TableEntry> tableEntries = new List<TableEntry>();

        /// <summary>
        /// Read-only table entries
        /// </summary>
        public IReadOnlyList<TableEntry> Entries => tableEntries;
        
        /// <summary>
        /// Editor preview of the weight in the table
        /// </summary>
        public float TotalWeightInTable { get; private set; }

        /// <summary>
        /// Editor preview of the dirty state flag
        /// </summary>
        private bool m_IsDirty = true;

        /// <inheritdoc />
        public T Read()
        {
            if (tableEntries == null || tableEntries.Count == 0)
            {
                return default(T);
            }

            if (m_IsDirty)
            {
                Recalculate();
            }

            var rng = random.Range(0, TotalWeightInTable);

            foreach (var entry in tableEntries)
            {
                if (rng > entry.LowBound && rng < entry.HighBound)
                {
                    return entry.value.Value;
                }
            }

            return default(T);
        }

        /// <summary>
        /// Add an entry to the table
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="weight">weight</param>
        public void Add(Ref<T> value, float weight)
        {
            tableEntries.Add(new TableEntry() { weight = weight, value = value });
            Recalculate();
        }

        /// <summary>
        /// Add an entry to the table
        /// </summary>
        /// <param name="value">direct value</param>
        /// <param name="weight">weight</param>
        public void Add(T value, float weight)
        {
            Add(new Ref<T>(value), weight);
        }

        /// <summary>
        /// Clear the table
        /// </summary>
        public void Clear()
        {
            tableEntries.Clear();
            Recalculate();
        }

        /// <summary>
        /// Unity hook for validation
        /// </summary>
        private void OnValidate()
        {
            Recalculate();
        }

        /// <summary>
        /// Internal calculation method
        /// </summary>
        private void Recalculate()
        {
            if (tableEntries == null || tableEntries.Count == 0)
            {
                return;
            }

            var maxWeight = 0f;

            foreach (var entry in tableEntries)
            {
                if (entry.weight >= 0f)
                {
                    entry.LowBound = maxWeight;
                    maxWeight += entry.weight;
                    entry.HighBound = maxWeight;
                }
                else
                {
                    entry.weight = 0f;
                }
            }

            TotalWeightInTable = maxWeight;

            foreach (var entry in tableEntries)
            {
                entry.Chance = (entry.weight / TotalWeightInTable) * 100;
            }

            m_IsDirty = false;
        }
    }
}