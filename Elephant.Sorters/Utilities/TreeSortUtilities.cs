namespace Elephant.Sorters.Utilities
{
	/// <summary>
	/// Tree sort utilities.
	/// </summary>
	internal static class TreeSortUtilities
	{
		/// <summary>
		/// Represents a node within a binary search tree for the tree sort.
		/// </summary>
		/// <typeparam name="T">The type of value contained in the node.</typeparam>
		internal class TreeNode<T>
			where T : IComparable<T>
		{
			/// <summary>
			/// The value stored in the node.
			/// </summary>
			internal T Value { get; set; }

			/// <summary>
			/// The left child of the node, representing values less than this node's value.
			/// It can be null if there is no left child.
			/// </summary>
			internal TreeNode<T>? Left { get; set; } = null;

			/// <summary>
			/// The right child of the node, representing values greater than this node's value.
			/// It can be null if there is no right child.
			/// </summary>
			internal TreeNode<T>? Right { get; set; } = null;

			/// <summary>
			/// Initializes a new instance of the TreeNode class with the specified value.
			/// </summary>
			/// <param name="value">The value to store in the node.</param>
			internal TreeNode(T value)
			{
				Value = value;
			}
		}

		/// <summary>
		/// Represents a binary search tree for the tree sort.
		/// </summary>
		/// <typeparam name="T">The type of elements stored in this binary search tree.</typeparam>
		internal class BinarySearchTree<T>
			where T : IComparable<T>
		{
			/// <summary>
			/// The root node of the binary search tree. It can be null if the tree is empty.
			/// </summary>
			private TreeNode<T>? _root = null;

			/// <summary>
			/// Inserts a new value into the binary search tree. If the tree is empty, the new value becomes the root.
			/// </summary>
			/// <param name="value">Value to insert.</param>
			internal void Insert(T value)
			{
				_root = Insert(_root, value);
			}

			/// <summary>
			/// Recursively inserts a value into the binary search tree starting from a specific node.
			/// It returns the new or modified node as the root of the sub-tree.
			/// </summary>
			/// <param name="node">Starting node for insertion, which could be null if the sub-tree is empty.</param>
			/// <param name="value">Value to insert.</param>
			/// <returns>New root of the sub-tree.</returns>
			private TreeNode<T> Insert(TreeNode<T>? node, T value)
			{
				// If the current node is null, a new node containing the value is created and returned.
				if (node == null) return new TreeNode<T>(value);

				// Otherwise, recursively insert the value into the correct sub-tree based on its value.
				if (value.CompareTo(node.Value) < 0)
					node.Left = Insert(node.Left, value);
				else if (value.CompareTo(node.Value) > 0)
					node.Right = Insert(node.Right, value);

				// Return the node, with its potentially updated children.
				return node;
			}

			/// <summary>
			/// Performs an in-order traversal of the binary search tree, executing the provided action on each node's value.
			/// This method starts from the root of the tree.
			/// </summary>
			/// <param name="action">Action to perform on each node's value. Cannot be null.</param>
			internal void InOrderTraversal(Action<T> action)
			{
				// Perform the in-order traversal starting from the root.
				InOrderTraversal(_root, action);
			}

			/// <summary>
			/// Recursively performs an in-order traversal starting from a specific node,
			/// and applies an action to each visited node's value. If the starting node is null,
			/// the method returns immediately, doing nothing.
			/// </summary>
			/// <param name="node">Starting node for the traversal, which may be null if the sub-tree is empty.</param>
			/// <param name="action">Action to apply to each node's value. Cannot be null.</param>
			private void InOrderTraversal(TreeNode<T>? node, Action<T> action)
			{
				// If the node is null, return immediately (base case for the recursion).
				if (node != null)
				{
					// Traverse the left sub-tree, process the current node, then traverse the right sub-tree.
					InOrderTraversal(node.Left, action);
					action(node.Value);
					InOrderTraversal(node.Right, action);
				}
			}
		}
	}
}
