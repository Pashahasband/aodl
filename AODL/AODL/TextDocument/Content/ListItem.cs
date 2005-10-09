/*
 * $Id: ListItem.cs,v 1.1 2005/10/09 15:52:47 larsbm Exp $
 */

using System;
using System.Xml;
using AODL.TextDocument;
using AODL.TextDocument.Content;
using AODL.TextDocument.Style;
using AODL.TextDocument.Style.Properties;

namespace AODL.TextDocument.Content
{
	/// <summary>
	/// Zusammenfassung f�r ListItem.
	/// </summary>
	public class ListItem : IContent, IContentContainer
	{
		private Paragraph _paragraph;
		/// <summary>
		/// The ListItem has a Paragraph which represent his TextContent.
		/// </summary>
		public Paragraph Paragraph
		{
			get { return this._paragraph; }
			set 
			{ 
				this._paragraph = value; 
				if(this.Node.ChildNodes.Count != 0)
				{
					XmlNode xn	= this.Node.ChildNodes.Item(0);
					this.Node.RemoveChild(xn);
				}
				this.Node.AppendChild(((Paragraph)value).Node);
			}
		}

		private List _list;
		/// <summary>
		/// The List object to which this ListItem belongs.
		/// </summary>
		public List List
		{
			get { return this._list; }
			set { this._list = value; }
		}

		/// <summary>
		/// Create a new ListItem
		/// </summary>
		/// <param name="li">The List object to which this ListItem belongs.</param>
		public ListItem(List li)
		{
			this.List				= li;
			this.NewXmlNode(li.Document);
			this.Paragraph			= new Paragraph(li.Document, li.ParagraphStyle.Name);
			this.Content			= new IContentCollection();

			this.Content.Inserted	+=new AODL.Collections.CollectionWithEvents.CollectionChange(Content_Inserted);
		}

		/// <summary>
		/// Create a new XmlNode.
		/// </summary>
		/// <param name="td">The TextDocument.</param>
		private void NewXmlNode(TextDocument td)
		{			
			this.Node		= td.CreateNode("list-item", "text");
		}

		#region IContent Member

		private TextDocument _document;
		/// <summary>
		/// The TextDocument to this ListItem belongs.
		/// </summary>
		public TextDocument Document
		{
			get
			{
				return this._document;
			}
			set
			{
				this._document = value;
			}
		}

		private XmlNode _node;
		/// <summary>
		/// The XmlNode.
		/// </summary>
		public XmlNode Node
		{
			get
			{
				return this._node;
			}
			set
			{
				this._node = value;
			}
		}

		/// <summary>
		/// No style element for ListItem!
		/// Throws Exception if you try to get it!;
		/// </summary>
		public string Stylename
		{
			get
			{
				throw new NotSupportedException("The IStyle interface is not supported by ListItem!");
			}
			set
			{
				throw new NotSupportedException("The IStyle interface is not supported by ListItem!");
			}
		}

		/// <summary>
		/// IStyle object is not supported. Throws NotSupportedException.
		/// </summary>
		public IStyle Style
		{
			get
			{
				throw new NotSupportedException("The IStyle interface is not supported by ListItem!");
			}
			set
			{
				throw new NotSupportedException("The IStyle interface is not supported by ListItem!");
			}
		}

		/// <summary>
		/// The ListItem doesn't have a ITextCollection. Instead the List
		/// Item has a Paragraph and his TextContent represents the Text
		/// for the ListItem.
		/// Properties Set Method throws NotSupportedException.
		/// Properties Get Method return the Paragraphs ITextCollection.
		/// </summary>
		public ITextCollection TextContent
		{
			get
			{				
				return this.Paragraph.TextContent;
			}
			set
			{
				throw new NotSupportedException("Setting the ITextCollection is not supported by ListItem!");
			}
		}

		#endregion

		#region IContentContainer Member

		private IContentCollection _content;
		/// <summary>
		/// ContentCollection, a ListItem could have
		/// another List inside (Inner List)
		/// </summary>
		public IContentCollection Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		#endregion

		private void Content_Inserted(int index, object value)
		{
			this.Node.AppendChild(((IContent)value).Node);
		}
	}
}

/*
 * $Log: ListItem.cs,v $
 * Revision 1.1  2005/10/09 15:52:47  larsbm
 * - Changed some design at the paragraph usage
 * - add list support
 *
 */