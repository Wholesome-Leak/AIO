using System;
using System.Collections;
using System.Collections.Generic;

namespace AIO.Helpers
{
	// Token: 0x0200002D RID: 45
	public class RingBuffer<T> : IEnumerable<T>, IEnumerable, ICollection<T>, ICollection
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000B198 File Offset: 0x00009398
		public bool AllowOverflow { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B1A0 File Offset: 0x000093A0
		public int Capacity
		{
			get
			{
				return this.Buffer.Length;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000B1AA File Offset: 0x000093AA
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000B1B2 File Offset: 0x000093B2
		public int Size { get; private set; } = 0;

		// Token: 0x06000288 RID: 648 RVA: 0x0000B1BC File Offset: 0x000093BC
		public T Get()
		{
			bool flag = this.Size == 0;
			if (flag)
			{
				throw new InvalidOperationException("Buffer is empty.");
			}
			T result = this.Buffer[this.Head];
			this.Head = (this.Head + 1) % this.Capacity;
			int size = this.Size;
			this.Size = size - 1;
			return result;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B220 File Offset: 0x00009420
		public void Put(T item)
		{
			bool flag = this.Tail == this.Head && this.Size != 0;
			if (flag)
			{
				bool allowOverflow = this.AllowOverflow;
				if (!allowOverflow)
				{
					throw new InvalidOperationException("The RingBuffer is full");
				}
				this.AddToBuffer(item, true);
			}
			else
			{
				this.AddToBuffer(item, false);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B280 File Offset: 0x00009480
		private void AddToBuffer(T toAdd, bool overflow)
		{
			if (overflow)
			{
				this.Head = (this.Head + 1) % this.Capacity;
			}
			else
			{
				int size = this.Size;
				this.Size = size + 1;
			}
			this.Buffer[this.Tail] = toAdd;
			this.Tail = (this.Tail + 1) % this.Capacity;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B2E6 File Offset: 0x000094E6
		public RingBuffer() : this(4)
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000B2F1 File Offset: 0x000094F1
		public RingBuffer(int capacity) : this(capacity, false)
		{
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000B2FD File Offset: 0x000094FD
		public RingBuffer(int capacity, bool overflow)
		{
			this.Buffer = new T[capacity];
			this.AllowOverflow = overflow;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000B32F File Offset: 0x0000952F
		public IEnumerator<T> GetEnumerator()
		{
			int _index = this.Head;
			int i = 0;
			while (i < this.Size)
			{
				yield return this.Buffer[_index];
				int num = i;
				i = num + 1;
				_index = (_index + 1) % this.Capacity;
			}
			yield break;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B340 File Offset: 0x00009540
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B358 File Offset: 0x00009558
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000B370 File Offset: 0x00009570
		public int Count
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00008DB9 File Offset: 0x00006FB9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B378 File Offset: 0x00009578
		public void Add(T item)
		{
			this.Put(item);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B384 File Offset: 0x00009584
		public bool Contains(T item)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int num = this.Head;
			int i = 0;
			while (i < this.Size)
			{
				bool flag = @default.Equals(item, this.Buffer[num]);
				if (flag)
				{
					return true;
				}
				i++;
				num = (num + 1) % this.Capacity;
			}
			return false;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000B3E8 File Offset: 0x000095E8
		public void Clear()
		{
			for (int i = 0; i < this.Capacity; i++)
			{
				this.Buffer[i] = default(T);
			}
			this.Head = 0;
			this.Tail = 0;
			this.Size = 0;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B438 File Offset: 0x00009638
		public void CopyTo(T[] array, int arrayIndex)
		{
			int num = this.Head;
			int i = 0;
			while (i < this.Size)
			{
				array[arrayIndex] = this.Buffer[num];
				i++;
				arrayIndex++;
				num = (num + 1) % this.Capacity;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B488 File Offset: 0x00009688
		public bool Remove(T item)
		{
			int num = this.Head;
			int num2 = 0;
			bool flag = false;
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int i = 0;
			while (i < this.Size)
			{
				bool flag2 = @default.Equals(item, this.Buffer[num]);
				if (flag2)
				{
					num2 = num;
					flag = true;
					break;
				}
				i++;
				num = (num + 1) % this.Capacity;
			}
			bool flag3 = flag;
			bool result;
			if (flag3)
			{
				T[] array = new T[this.Size - 1];
				num = this.Head;
				bool flag4 = false;
				int j = 0;
				while (j < this.Size - 1)
				{
					bool flag5 = num == num2;
					if (flag5)
					{
						flag4 = true;
					}
					bool flag6 = flag4;
					if (flag6)
					{
						array[num] = this.Buffer[(num + 1) % this.Capacity];
					}
					else
					{
						array[num] = this.Buffer[num];
					}
					j++;
					num = (num + 1) % this.Capacity;
				}
				int size = this.Size;
				this.Size = size - 1;
				this.Buffer = array;
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B5B2 File Offset: 0x000097B2
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008DB9 File Offset: 0x00006FB9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000B5B5 File Offset: 0x000097B5
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((T[])array, index);
		}

		// Token: 0x04000134 RID: 308
		private int Head = 0;

		// Token: 0x04000135 RID: 309
		private int Tail = 0;

		// Token: 0x04000136 RID: 310
		private T[] Buffer;
	}
}
