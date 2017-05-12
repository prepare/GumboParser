
// Copyright 2010 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Author: jdtang@google.com (Jonathan Tang)


namespace Gumbo
{
    using System.Text;
    using System.Collections.Generic;

    //// Because GumboStringPieces are immutable, we can't insert a character directly
    //// into a text node.  Instead, we accumulate all pending characters here and
    //// flush them out to a text node whenever a new element is inserted.
    ////
    //// http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#insert-a-character
    //typedef struct _TextNodeBufferState
    //{
    //    // The accumulated text to be inserted into the current text node.
    //    GumboStringBuffer _buffer;

    //    // A pointer to the original text represented by this text node.  Note that
    //    // because of foster parenting and other strange DOM manipulations, this may
    //    // include other non-text HTML tags in it; it is defined as the span of
    //    // original text from the first character in this text node to the last
    //    // character in this text node.
    //    const char* _start_original_text;

    //    // The source position of the start of this text node.
    //    GumboSourcePosition _start_position;

    //    // The type of node that will be inserted (TEXT, CDATA, or WHITESPACE).
    //    GumboNodeType _type;
    //}
    //TextNodeBufferState;

    class GumboStringBuffer { }

    struct TextBufferPointer
    {

    }

    class TextNodeBufferState
    {
        public GumboStringBuffer _buffer;
        public TextBufferPointer _start_original_text;
        public GumboSourcePosition _start_position;
        public GumboNodeType _type;
    }

    class GumboParserState
    {
        // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#insertion-mode
        public GumboInsertionMode _insertion_mode;

        //    // Used for run_generic_parsing_algorithm, which needs to switch back to the
        //    // original insertion mode at its conclusion.
        public GumboInsertionMode _original_insertion_mode;

        //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-stack-of-open-elements
        //    GumboVector /*GumboNode*/ _open_elements;
        public List<GumboNode> _open_elements;

        //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-list-of-active-formatting-elements
        //    GumboVector /*GumboNode*/ _active_formatting_elements;
        public List<GumboNode> _active_formatting_elements;

        //    // The stack of template insertion modes.
        //    // http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#the-insertion-mode
        //    GumboVector /*InsertionMode*/ _template_insertion_modes;
        public List<GumboInsertionMode> _template_insertion_modes;


        //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-element-pointers
        //    GumboNode* _head_element;
        //    GumboNode* _form_element;

        public GumboNode _head_element;
        public GumboNode _form_element;

        //    // The element used as fragment context when parsing in fragment mode
        //    GumboNode* _fragment_ctx;
        public GumboNode _fragment_ctx;

        //    // The flag for when the spec says "Reprocess the current token in..."
        //    bool _reprocess_current_token;
        public bool _reprocess_current_token;
        //    // The flag for "acknowledge the token's self-closing flag".
        //    bool _self_closing_flag_acknowledged;
        public bool _self_closing_flag_acknowledged;

        //    // The "frameset-ok" flag from the spec.
        //    bool _frameset_ok;
        public bool _frameset_ok;
        //    // The flag for "If the next token is a LINE FEED, ignore that token...".
        //    bool _ignore_next_linefeed;
        public bool _ignore_next_linefeed;

        //    // The flag for "whenever a node would be inserted into the current node, it
        //    // must instead be foster parented".  This is used for misnested table
        //    // content, which needs to be handled according to "in body" rules yet foster
        //    // parented outside of the table.
        //    // It would perhaps be more explicit to have this as a parameter to
        //    // handle_in_body and insert_element, but given how special-purpose this is
        //    // and the number of call-sites that would need to take the extra parameter,
        //    // it's easier just to have a state flag.
        //    bool _foster_parent_insertions;
        public bool _foster_parent_insertions;
        //    // The accumulated text node buffer state.
        //    TextNodeBufferState _text_node;
        public TextNodeBufferState _text_node;
        //    // The current token.
        //    GumboToken* _current_token;
        public GumboToken _current_token;
        //    // The way that the spec is written, the </body> and </html> tags are *always*
        //    // implicit, because encountering one of those tokens merely switches the
        //    // insertion mode out of "in body".  So we have individual state flags for
        //    // those end tags that are then inspected by pop_current_node when the <body>
        //    // and <html> nodes are popped to set the GUMBO_INSERTION_IMPLICIT_END_TAG
        //    // flag appropriately.
        //    bool _closed_body_tag;
        //    bool _closed_html_tag;
        public bool _closed_body_tag;
        public bool _closed_html_tag;


    }
    //typedef struct GumboInternalParserState
    //{
    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#insertion-mode
    //    GumboInsertionMode _insertion_mode;

    //    // Used for run_generic_parsing_algorithm, which needs to switch back to the
    //    // original insertion mode at its conclusion.
    //    GumboInsertionMode _original_insertion_mode;

    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-stack-of-open-elements
    //    GumboVector /*GumboNode*/ _open_elements;

    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-list-of-active-formatting-elements
    //    GumboVector /*GumboNode*/ _active_formatting_elements;

    //    // The stack of template insertion modes.
    //    // http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#the-insertion-mode
    //    GumboVector /*InsertionMode*/ _template_insertion_modes;

    //    // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#the-element-pointers
    //    GumboNode* _head_element;
    //    GumboNode* _form_element;

    //    // The element used as fragment context when parsing in fragment mode
    //    GumboNode* _fragment_ctx;

    //    // The flag for when the spec says "Reprocess the current token in..."
    //    bool _reprocess_current_token;

    //    // The flag for "acknowledge the token's self-closing flag".
    //    bool _self_closing_flag_acknowledged;

    //    // The "frameset-ok" flag from the spec.
    //    bool _frameset_ok;

    //    // The flag for "If the next token is a LINE FEED, ignore that token...".
    //    bool _ignore_next_linefeed;

    //    // The flag for "whenever a node would be inserted into the current node, it
    //    // must instead be foster parented".  This is used for misnested table
    //    // content, which needs to be handled according to "in body" rules yet foster
    //    // parented outside of the table.
    //    // It would perhaps be more explicit to have this as a parameter to
    //    // handle_in_body and insert_element, but given how special-purpose this is
    //    // and the number of call-sites that would need to take the extra parameter,
    //    // it's easier just to have a state flag.
    //    bool _foster_parent_insertions;

    //    // The accumulated text node buffer state.
    //    TextNodeBufferState _text_node;

    //    // The current token.
    //    GumboToken* _current_token;

    //    // The way that the spec is written, the </body> and </html> tags are *always*
    //    // implicit, because encountering one of those tokens merely switches the
    //    // insertion mode out of "in body".  So we have individual state flags for
    //    // those end tags that are then inspected by pop_current_node when the <body>
    //    // and <html> nodes are popped to set the GUMBO_INSERTION_IMPLICIT_END_TAG
    //    // flag appropriately.
    //    bool _closed_body_tag;
    //    bool _closed_html_tag;
    //}
    //GumboParserState;


    //----------------------------
    partial class GumboParser
    {
        GumboParserState parser_state;

        public GumboParser()
        {

        }
        void parser_state_init()
        {
            //TODO: review how to alloc new object


            //from line: 476
            //static void parser_state_init(GumboParser* parser)
            //{
            //    GumboParserState* parser_state =
            //        gumbo_parser_allocate(parser, sizeof(GumboParserState));
            //    parser_state->_insertion_mode = GUMBO_INSERTION_MODE_INITIAL;
            //    parser_state->_reprocess_current_token = false;
            //    parser_state->_frameset_ok = true;
            //    parser_state->_ignore_next_linefeed = false;
            //    parser_state->_foster_parent_insertions = false;
            //    parser_state->_text_node._type = GUMBO_NODE_WHITESPACE;
            //    gumbo_string_buffer_init(parser, &parser_state->_text_node._buffer);
            //    gumbo_vector_init(parser, 10, &parser_state->_open_elements);
            //    gumbo_vector_init(parser, 5, &parser_state->_active_formatting_elements);
            //    gumbo_vector_init(parser, 5, &parser_state->_template_insertion_modes);
            //    parser_state->_head_element = NULL;
            //    parser_state->_form_element = NULL;
            //    parser_state->_fragment_ctx = NULL;
            //    parser_state->_current_token = NULL;
            //    parser_state->_closed_body_tag = false;
            //    parser_state->_closed_html_tag = false;
            //    parser->_parser_state = parser_state;
            //}

            GumboParserState parser_state = new GumboParserState();
            parser_state._insertion_mode = GumboInsertionMode.GUMBO_INSERTION_MODE_INITIAL;
            parser_state._reprocess_current_token = false;
            parser_state._frameset_ok = true;
            parser_state._ignore_next_linefeed = false;
            parser_state._foster_parent_insertions = false;
            parser_state._text_node._type = GumboNodeType.GUMBO_NODE_WHITESPACE;
            parser_state._text_node._buffer = new GumboStringBuffer();
            parser_state._open_elements = new List<GumboNode>(10);
            parser_state._active_formatting_elements = new List<GumboNode>(5);
            parser_state._template_insertion_modes = new List<GumboInsertionMode>(5);
            //
            //... else fields are set to its default in C#
            this.parser_state = parser_state;
        }
        void parser_state_destroy()
        {
            //static void parser_state_destroy(GumboParser* parser)
            //{
            //    GumboParserState* state = parser->_parser_state;
            //    if (state->_fragment_ctx)
            //    {
            //        destroy_node(parser, state->_fragment_ctx);
            //    }
            //    gumbo_vector_destroy(parser, &state->_active_formatting_elements);
            //    gumbo_vector_destroy(parser, &state->_open_elements);
            //    gumbo_vector_destroy(parser, &state->_template_insertion_modes);
            //    gumbo_string_buffer_destroy(parser, &state->_text_node._buffer);
            //    gumbo_parser_deallocate(parser, state);
            //}
        }
        public GumboNode get_document_node()
        {
            //static GumboNode* get_document_node(GumboParser* parser)
            //{
            //    return parser->_output->document;
            //}
            return this._output.document;
        }

        public bool is_fragment_parser()
        {     //static GumboNode* get_document_node(GumboParser* parser)
              //{
              //    return parser->_output->document;
              //}
              //        static bool is_fragment_parser(const GumboParser* parser) {
              //  return !!parser->_parser_state->_fragment_ctx;
              //}
            return this.parser_state._fragment_ctx != null;
        }

        public GumboNode get_current_node()
        {
            //// Returns the node at the bottom of the stack of open elements, or NULL if no
            //// elements have been added yet.
            //static GumboNode* get_current_node(GumboParser* parser)
            //{
            //    GumboVector* open_elements = &parser->_parser_state->_open_elements;
            //    if (open_elements->length == 0)
            //    {
            //        assert(!parser->_output->root);
            //        return NULL;
            //    }
            //    assert(open_elements->length > 0);
            //    assert(open_elements->data != NULL);
            //    return open_elements->data[open_elements->length - 1];
            //}
            List<GumboNode> open_elements = this.parser_state._open_elements;
            if (open_elements.Count == 0)
            {
                // assert(!parser->_output->root);
                return null;
            }
            return open_elements[open_elements.Count - 1];
        }
        public GumboNode get_adjusted_current_node()
        {
            //static GumboNode* get_adjusted_current_node(GumboParser* parser)
            //{
            //    GumboParserState* state = parser->_parser_state;
            //    if (state->_open_elements.length == 1 && state->_fragment_ctx)
            //    {
            //        return state->_fragment_ctx;
            //    }
            //    return get_current_node(parser);
            //}
            GumboParserState state = this._parser_state;
            if (state._open_elements.Count == 1 && state._fragment_ctx != null)
            {
                return state._fragment_ctx;
            }
            return get_current_node();
        }

        public bool is_in_static_list(string needle, GumboStringPiece haystack, bool exact_match)
        {
            //        // Returns true if the given needle is in the given array of literal
            //        // GumboStringPieces.  If exact_match is true, this requires that they match
            //        // exactly; otherwise, this performs a prefix match to check if any of the
            //        // elements in haystack start with needle.  This always performs a
            //        // case-insensitive match.
            //        static bool is_in_static_list(
            //    const char* needle, const GumboStringPiece* haystack, bool exact_match) {
            //  for (unsigned int i = 0; haystack[i].length > 0; ++i) {
            //    if ((exact_match && !strcmp(needle, haystack[i].data)) ||
            //        (!exact_match && !strcasecmp(needle, haystack[i].data))) {
            //      return true;
            //    }
            //  }
            //  return false;
            //}
            throw new System.NotImplementedException();
            return false;
        }
        public void set_insertion_mode(GumboInsertionMode mode)
        {
            //public static void set_insertion_mode(GumboParser* parser, GumboInsertionMode mode)
            //{
            //    parser->_parser_state->_insertion_mode = mode;
            //}
            _parser_state._insertion_mode = mode;
        }



        //        // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#reset-the-insertion-mode-appropriately
        //        // This is a helper function that returns the appropriate insertion mode instead
        //        // of setting it.  Returns GUMBO_INSERTION_MODE_INITIAL as a sentinel value to
        //        // indicate that there is no appropriate insertion mode, and the loop should
        //        // continue.
        //static GumboInsertionMode get_appropriate_insertion_mode(
        //      const GumboParser* parser, int index) {
        //
        //      const GumboVector* open_elements = &parser->_parser_state->_open_elements;
        //        const GumboNode* node = open_elements->data[index];
        //        const bool is_last = index == 0;

        //  if (is_last && is_fragment_parser(parser)) {
        //    node = parser->_parser_state->_fragment_ctx;
        //  }

        //  assert(node->type == GUMBO_NODE_ELEMENT || node->type == GUMBO_NODE_TEMPLATE);
        //  if (node->v.element.tag_namespace != GUMBO_NAMESPACE_HTML)
        //    return is_last?
        //      GUMBO_INSERTION_MODE_IN_BODY : GUMBO_INSERTION_MODE_INITIAL;

        //  switch (node->v.element.tag) {
        //    case GUMBO_TAG_SELECT: {
        //      if (is_last) {
        //        return GUMBO_INSERTION_MODE_IN_SELECT;
        //      }
        //      for (int i = index; i > 0; --i) {
        //        const GumboNode* ancestor = open_elements->data[i];
        //        if (node_html_tag_is(ancestor, GUMBO_TAG_TEMPLATE)) {
        //          return GUMBO_INSERTION_MODE_IN_SELECT;
        //        }
        //        if (node_html_tag_is(ancestor, GUMBO_TAG_TABLE)) {
        //          return GUMBO_INSERTION_MODE_IN_SELECT_IN_TABLE;
        //        }
        //      }
        //      return GUMBO_INSERTION_MODE_IN_SELECT;
        //    }
        //    case GUMBO_TAG_TD:
        //    case GUMBO_TAG_TH:
        //      if (!is_last) return GUMBO_INSERTION_MODE_IN_CELL;
        //      break;
        //    case GUMBO_TAG_TR:
        //      return GUMBO_INSERTION_MODE_IN_ROW;
        //    case GUMBO_TAG_TBODY:
        //    case GUMBO_TAG_THEAD:
        //    case GUMBO_TAG_TFOOT:
        //      return GUMBO_INSERTION_MODE_IN_TABLE_BODY;
        //    case GUMBO_TAG_CAPTION:
        //      return GUMBO_INSERTION_MODE_IN_CAPTION;
        //    case GUMBO_TAG_COLGROUP:
        //      return GUMBO_INSERTION_MODE_IN_COLUMN_GROUP;
        //    case GUMBO_TAG_TABLE:
        //      return GUMBO_INSERTION_MODE_IN_TABLE;
        //    case GUMBO_TAG_TEMPLATE:
        //      return get_current_template_insertion_mode(parser);
        //    case GUMBO_TAG_HEAD:
        //      if (!is_last) return GUMBO_INSERTION_MODE_IN_HEAD;
        //      break;
        //    case GUMBO_TAG_BODY:
        //      return GUMBO_INSERTION_MODE_IN_BODY;
        //    case GUMBO_TAG_FRAMESET:
        //      return GUMBO_INSERTION_MODE_IN_FRAMESET;
        //    case GUMBO_TAG_HTML:
        //      return parser->_parser_state->_head_element
        //                 ? GUMBO_INSERTION_MODE_AFTER_HEAD
        //                 : GUMBO_INSERTION_MODE_BEFORE_HEAD;
        //    default:
        //      break;
        //  }
        //  return is_last? GUMBO_INSERTION_MODE_IN_BODY : GUMBO_INSERTION_MODE_INITIAL;
        //}
        public GumboInsertionMode get_appropriate_insertion_mode(int index)
        {
            //line: 630
            //      const GumboVector* open_elements = &parser->_parser_state->_open_elements;
            //        const GumboNode* node = open_elements->data[index];
            //        const bool is_last = index == 0;

            List<GumboNode> open_elements = _parser_state._open_elements;
            GumboNode node = open_elements[index];
            bool is_last = index == 0;
            //  if (is_last && is_fragment_parser(parser)) {
            //    node = parser->_parser_state->_fragment_ctx;
            //  }

            if (is_last && is_fragment_parser())
            {
                node = _parser_state._fragment_ctx;
            }
            //TODO:
            //  assert(node->type == GUMBO_NODE_ELEMENT || node->type == GUMBO_NODE_TEMPLATE);

            GumboInternalNode internalElemen = (GumboInternalNode)node;

            if (internalElemen.element.tag_namespace != GumboNamespaceEnum.GUMBO_NAMESPACE_HTML)
                return is_last ?
                 GumboInsertionMode.GUMBO_INSERTION_MODE_IN_BODY : GumboInsertionMode.GUMBO_INSERTION_MODE_INITIAL;
            switch (internalElemen.element.tag)
            {
                case GumboTag.GUMBO_TAG_SELECT:
                    {
                        if (is_last)
                        {
                            return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_SELECT;
                        }
                        for (int i = index; i > 0; --i)
                        {
                            GumboNode ancestor = open_elements[i];
                            if (ancestor.node_html_tag_is(GumboTag.GUMBO_TAG_TEMPLATE))
                            {
                                return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_SELECT;
                            }
                            if (ancestor.node_html_tag_is(GumboTag.GUMBO_TAG_TABLE))
                            {
                                return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_SELECT_IN_TABLE;
                            }
                        }
                        return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_SELECT;
                    }
                case GumboTag.GUMBO_TAG_TD:
                case GumboTag.GUMBO_TAG_TH:
                    if (!is_last) return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_CELL;
                    break;
                case GumboTag.GUMBO_TAG_TR:
                    return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_ROW;
                case GumboTag.GUMBO_TAG_TBODY:
                case GumboTag.GUMBO_TAG_THEAD:
                case GumboTag.GUMBO_TAG_TFOOT:
                    return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_TABLE_BODY;
                case GumboTag.GUMBO_TAG_CAPTION:
                    return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_CAPTION;
                case GumboTag.GUMBO_TAG_COLGROUP:
                    return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_COLUMN_GROUP;
                case GumboTag.GUMBO_TAG_TABLE:
                    return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_TABLE;
                case GumboTag.GUMBO_TAG_TEMPLATE:
                    return get_current_template_insertion_mode();
                case GumboTag.GUMBO_TAG_HEAD:
                    if (!is_last) return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_HEAD;
                    break;
                case GumboTag.GUMBO_TAG_BODY:
                    return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_BODY;
                case GumboTag.GUMBO_TAG_FRAMESET:
                    return GumboInsertionMode.GUMBO_INSERTION_MODE_IN_FRAMESET;
                case GumboTag.GUMBO_TAG_HTML:
                    return (_parser_state._head_element != null)
                               ? GumboInsertionMode.GUMBO_INSERTION_MODE_AFTER_HEAD
                               : GumboInsertionMode.GUMBO_INSERTION_MODE_BEFORE_HEAD;
                default:
                    break;
            }
            return is_last ? GumboInsertionMode.GUMBO_INSERTION_MODE_IN_BODY : GumboInsertionMode.GUMBO_INSERTION_MODE_INITIAL;
        }



        GumboError parser_add_parse_error(List<GumboToken> tokens)
        {

            //line: 645
            //static GumboError* parser_add_parse_error(
            //    GumboParser* parser, const GumboToken* token) {
            //  gumbo_debug("Adding parse error.\n");
            //  GumboError* error = gumbo_add_error(parser);
            //  if (!error) {
            //    return NULL;
            //  }
            //    error->type = GUMBO_ERR_PARSER;
            //  error->position = token->position;
            //  error->original_text = token->original_text.data;
            //  GumboParserError* extra_data = &error->v.parser;
            //    extra_data->input_type = token->type;
            //  extra_data->input_tag = GUMBO_TAG_UNKNOWN;
            //  if (token->type == GUMBO_TOKEN_START_TAG) {
            //    extra_data->input_tag = token->v.start_tag.tag;
            //  } else if (token->type == GUMBO_TOKEN_END_TAG) {
            //    extra_data->input_tag = token->v.end_tag;
            //  }
            //  GumboParserState* state = parser->_parser_state;
            //extra_data->parser_state = state->_insertion_mode;
            //  gumbo_vector_init(
            //      parser, state->_open_elements.length, &extra_data->tag_stack);
            //  for (unsigned int i = 0; i<state->_open_elements.length; ++i) {
            //    const GumboNode* node = state->_open_elements.data[i];
            //    assert(
            //        node->type == GUMBO_NODE_ELEMENT || node->type == GUMBO_NODE_TEMPLATE);
            //    gumbo_vector_add(
            //        parser, (void*) node->v.element.tag, &extra_data->tag_stack);
            //  }
            //  return error;
            //}
            //-------------------------
            throw new TODOImplementException();
            return null;
        }



        bool tag_in()
        {
            //line:676:
            //        // Returns true if the specified token is either a start or end tag (specified
            //        // by is_start) with one of the tag types in the varargs list.  Terminate the
            //        // list with GUMBO_TAG_LAST; this functions as a sentinel since no portion of
            //        // the spec references tags that are not in the spec.
            //        static bool tag_in(
            //    const GumboToken* token, bool is_start, const gumbo_tagset tags) {
            //  GumboTag token_tag;
            //  if (is_start && token->type == GUMBO_TOKEN_START_TAG) {
            //    token_tag = token->v.start_tag.tag;
            //  } else if (!is_start && token->type == GUMBO_TOKEN_END_TAG) {
            //    token_tag = token->v.end_tag;
            //  } else {
            //    return false;
            //  }
            //  return (token_tag<GUMBO_TAG_LAST && tags[(int)token_tag] != 0);
            //}
            throw new TODOImplementException();
            return false;
        }
        bool tag_is()
        {
            //line:695
            //// Like tag_in, but for the single-tag case.
            //  static bool tag_is(const GumboToken* token, bool is_start, GumboTag tag) {
            //  if (is_start && token->type == GUMBO_TOKEN_START_TAG) {
            //    return token->v.start_tag.tag == tag;
            //  } else if (!is_start && token->type == GUMBO_TOKEN_END_TAG) {
            //    return token->v.end_tag == tag;
            //  } else {
            //    return false;
            //  }
            //}


            throw new TODOImplementException();
        }

        bool node_tag_in_set()
        {
            //line:706
            //      // Like tag_in, but checks for the tag of a node, rather than a token.
            //      static bool node_tag_in_set(const GumboNode* node, const gumbo_tagset tags) {
            //assert(node != NULL);
            //if (node->type != GUMBO_NODE_ELEMENT && node->type != GUMBO_NODE_TEMPLATE) {
            //  return false;
            //}
            //return TAGSET_INCLUDES(
            //    tags, node->v.element.tag_namespace, node->v.element.tag);
            //  }
            throw new TODOImplementException();
        }

        bool node_qualified_tag_is()
        {
            //line: 716
            //        // Like node_tag_in, but for the single-tag case.
            //        static bool node_qualified_tag_is(
            //    const GumboNode* node, GumboNamespaceEnum ns, GumboTag tag) {
            //  assert(node);
            //  return (node->type == GUMBO_NODE_ELEMENT ||
            //             node->type == GUMBO_NODE_TEMPLATE) &&
            //         node->v.element.tag == tag && node->v.element.tag_namespace == ns;
            //}
            throw new TODOImplementException();
        }


        bool node_html_tag_is()
        {
            //line: 726
            //      // Like node_tag_in, but for the single-tag case in the HTML namespace
            //      static bool node_html_tag_is(const GumboNode* node, GumboTag tag) {
            //return node_qualified_tag_is(node, GUMBO_NAMESPACE_HTML, tag);
            //  }
            throw new TODOImplementException();
        }
        void push_template_insertion_mode()
        {
            //line : 729
            //    static void push_template_insertion_mode(
            //GumboParser* parser, GumboInsertionMode mode)
            //    {
            //        gumbo_vector_add(
            //            parser, (void*)mode, &parser->_parser_state->_template_insertion_modes);
            //    }
            throw new TODOImplementException();
        }
        void pop_template_insertion_mode()
        {
            //line: 735
            //static void pop_template_insertion_mode(GumboParser* parser)
            //{
            //    gumbo_vector_pop(parser, &parser->_parser_state->_template_insertion_modes);
            //}
            throw new TODOImplementException();
        }



        GumboInsertionMode get_current_template_insertion_mode()
        {
            //line: 741
            //        // Returns the current template insertion mode.  If the stack of template
            //        // insertion modes is empty, this returns GUMBO_INSERTION_MODE_INITIAL.
            //        static GumboInsertionMode get_current_template_insertion_mode(
            //    const GumboParser* parser) {
            //  GumboVector* template_insertion_modes =
            //      &parser->_parser_state->_template_insertion_modes;
            //  if (template_insertion_modes->length == 0) {
            //    return GUMBO_INSERTION_MODE_INITIAL;
            //  }
            //  return (GumboInsertionMode)
            //      template_insertion_modes->data[(template_insertion_modes->length - 1)];
            //}
            List<GumboInsertionMode> template_insertion_modes = _parser_state._template_insertion_modes;
            if (template_insertion_modes.Count == 0)
            {
                return GumboInsertionMode.GUMBO_INSERTION_MODE_INITIAL;
            }
            return template_insertion_modes[template_insertion_modes.Count - 1];
        }
        static bool is_mathml_integration_point()
        {
            //line: 754
            // // http://www.whatwg.org/specs/web-apps/current-work/multipage/tree-construction.html#mathml-text-integration-point
            //        static bool is_mathml_integration_point(const GumboNode* node) {
            //  return node_tag_in_set(
            //      node, (gumbo_tagset){
            //            TAG_MATHML(MI), TAG_MATHML(MO), TAG_MATHML(MN),
            //                TAG_MATHML(MS), TAG_MATHML(MTEXT)});
            //}

            throw new TODOImplementException();
        }
        static bool is_html_integration_point()
        {
            throw new TODOImplementException();
            //line: 761
            //        // http://www.whatwg.org/specs/web-apps/current-work/multipage/tree-construction.html#html-integration-point
            //        static bool is_html_integration_point(const GumboNode* node) {
            //  return node_tag_in_set(node, (gumbo_tagset){
            //            TAG_SVG(FOREIGNOBJECT),
            //                                   TAG_SVG(DESC), TAG_SVG(TITLE)}) ||
            //         (node_qualified_tag_is(
            //              node, GUMBO_NAMESPACE_MATHML, GUMBO_TAG_ANNOTATION_XML) &&
            //             (attribute_matches(
            //                  &node->v.element.attributes, "encoding", "text/html") ||
            //                 attribute_matches(&node->v.element.attributes, "encoding",
            //                     "application/xhtml+xml")));
            //}
        }
        //line: 774 => struct InsertionLocation

        InsertionLocation get_appropriate_insertion_location()
        {
            throw new TODOImplementException();
            //line: 781
            //InsertionLocation get_appropriate_insertion_location(
            //    GumboParser* parser, GumboNode* override_target)
            //{
            //    InsertionLocation retval = { override_target, -1 };
            //    if (retval.target == NULL)
            //    {
            //        // No override target; default to the current node, but special-case the
            //        // root node since get_current_node() assumes the stack of open elements is
            //        // non-empty.
            //        retval.target = parser->_output->root != NULL ? get_current_node(parser)
            //                                                      : get_document_node(parser);
            //    }
            //    if (!parser->_parser_state->_foster_parent_insertions ||
            //        !node_tag_in_set(retval.target, (gumbo_tagset){
            //        TAG(TABLE), TAG(TBODY),
            //                                  TAG(TFOOT), TAG(THEAD), TAG(TR)})) {
            //        return retval;
            //    }

            //    // Foster-parenting case.
            //    int last_template_index = -1;
            //    int last_table_index = -1;
            //    GumboVector* open_elements = &parser->_parser_state->_open_elements;
            //    for (unsigned int i = 0; i < open_elements->length; ++i)
            //    {
            //        if (node_html_tag_is(open_elements->data[i], GUMBO_TAG_TEMPLATE))
            //        {
            //            last_template_index = i;
            //        }
            //        if (node_html_tag_is(open_elements->data[i], GUMBO_TAG_TABLE))
            //        {
            //            last_table_index = i;
            //        }
            //    }
            //    if (last_template_index != -1 &&
            //        (last_table_index == -1 || last_template_index > last_table_index))
            //    {
            //        retval.target = open_elements->data[last_template_index];
            //        return retval;
            //    }
            //    if (last_table_index == -1)
            //    {
            //        retval.target = open_elements->data[0];
            //        return retval;
            //    }
            //    GumboNode* last_table = open_elements->data[last_table_index];
            //    if (last_table->parent != NULL)
            //    {
            //        retval.target = last_table->parent;
            //        retval.index = last_table->index_within_parent;
            //        return retval;
            //    }

            //    retval.target = open_elements->data[last_table_index - 1];
            //    return retval;
            //}

        }

        void append_node()
        {
            throw new TODOImplementException();
            //line: 829
            //// Appends a node to the end of its parent, setting the "parent" and
            //// "index_within_parent" fields appropriately.
            //static void append_node(
            //    GumboParser* parser, GumboNode* parent, GumboNode* node)
            //{
            //    assert(node->parent == NULL);
            //    assert(node->index_within_parent == -1);
            //    GumboVector* children;
            //    if (parent->type == GUMBO_NODE_ELEMENT ||
            //        parent->type == GUMBO_NODE_TEMPLATE)
            //    {
            //        children = &parent->v.element.children;
            //    }
            //    else
            //    {
            //        assert(parent->type == GUMBO_NODE_DOCUMENT);
            //        children = &parent->v.document.children;
            //    }
            //    node->parent = parent;
            //    node->index_within_parent = children->length;
            //    gumbo_vector_add(parser, (void*)node, children);
            //    assert(node->index_within_parent < children->length);
            //}
        }

        void insert_node()
        {
            throw new TODOImplementException();
            //line: 850
            //// Inserts a node at the specified InsertionLocation, updating the
            //// "parent" and "index_within_parent" fields of it and all its siblings.
            //// If the index of the location is -1, this calls append_node.
            //static void insert_node(
            //    GumboParser* parser, GumboNode* node, InsertionLocation location)
            //{
            //    assert(node->parent == NULL);
            //    assert(node->index_within_parent == -1);
            //    GumboNode* parent = location.target;
            //    int index = location.index;
            //    if (index != -1)
            //    {
            //        GumboVector* children = NULL;
            //        if (parent->type == GUMBO_NODE_ELEMENT ||
            //            parent->type == GUMBO_NODE_TEMPLATE)
            //        {
            //            children = &parent->v.element.children;
            //        }
            //        else if (parent->type == GUMBO_NODE_DOCUMENT)
            //        {
            //            children = &parent->v.document.children;
            //            assert(children->length == 0);
            //        }
            //        else
            //        {
            //            assert(0);
            //        }

            //        assert(index >= 0);
            //        assert((unsigned int) index < children->length);
            //        node->parent = parent;
            //        node->index_within_parent = index;
            //        gumbo_vector_insert_at(parser, (void*)node, index, children);
            //        assert(node->index_within_parent < children->length);
            //        for (unsigned int i = index + 1; i < children->length; ++i)
            //        {
            //            GumboNode* sibling = children->data[i];
            //            sibling->index_within_parent = i;
            //            assert(sibling->index_within_parent < children->length);
            //        }
            //    }
            //    else
            //    {
            //        append_node(parser, parent, node);
            //    }
            //} 
        }
        static void maybe_flush_text_node_buffer()
        {

            throw new TODOImplementException();
            //line: 884
            //static void maybe_flush_text_node_buffer(GumboParser* parser)
            //{
            //    GumboParserState* state = parser->_parser_state;
            //    TextNodeBufferState* buffer_state = &state->_text_node;
            //    if (buffer_state->_buffer.length == 0)
            //    {
            //        return;
            //    }

            //    assert(buffer_state->_type == GUMBO_NODE_WHITESPACE ||
            //           buffer_state->_type == GUMBO_NODE_TEXT ||
            //           buffer_state->_type == GUMBO_NODE_CDATA);
            //    GumboNode* text_node = create_node(parser, buffer_state->_type);
            //    GumboText* text_node_data = &text_node->v.text;
            //    text_node_data->text =
            //        gumbo_string_buffer_to_string(parser, &buffer_state->_buffer);
            //    text_node_data->original_text.data = buffer_state->_start_original_text;
            //    text_node_data->original_text.length =
            //        state->_current_token->original_text.data -
            //        buffer_state->_start_original_text;
            //    text_node_data->start_pos = buffer_state->_start_position;

            //    gumbo_debug("Flushing text node buffer of %.*s.\n",
            //        (int)buffer_state->_buffer.length, buffer_state->_buffer.data);

            //    InsertionLocation location = get_appropriate_insertion_location(parser, NULL);
            //    if (location.target->type == GUMBO_NODE_DOCUMENT)
            //    {
            //        // The DOM does not allow Document nodes to have Text children, so per the
            //        // spec, they are dropped on the floor.
            //        destroy_node(parser, text_node);
            //    }
            //    else
            //    {
            //        insert_node(parser, text_node, location);
            //    }

            //    gumbo_string_buffer_clear(parser, &buffer_state->_buffer);
            //    buffer_state->_type = GUMBO_NODE_WHITESPACE;
            //    assert(buffer_state->_buffer.length == 0);
            //}
        }
        void record_end_of_element()
        {
            throw new TODOImplementException();
            //line: 921
            //    static void record_end_of_element(
            //GumboToken* current_token, GumboElement* element)
            //    {
            //        element->end_pos = current_token->position;
            //        element->original_end_tag = current_token->type == GUMBO_TOKEN_END_TAG
            //                                        ? current_token->original_text
            //                                        : kGumboEmptyString;
            //    }
        }
        void pop_current_node()
        {
            throw new TODOImplementException();
            //line: 930
            //static GumboNode* pop_current_node(GumboParser* parser)
            //{
            //    GumboParserState* state = parser->_parser_state;
            //    maybe_flush_text_node_buffer(parser);
            //    if (state->_open_elements.length > 0)
            //    {
            //        assert(node_html_tag_is(state->_open_elements.data[0], GUMBO_TAG_HTML));
            //        gumbo_debug("Popping %s node.\n",
            //            gumbo_normalized_tagname(get_current_node(parser)->v.element.tag));
            //    }
            //    GumboNode* current_node = gumbo_vector_pop(parser, &state->_open_elements);
            //    if (!current_node)
            //    {
            //        assert(state->_open_elements.length == 0);
            //        return NULL;
            //    }
            //    assert(current_node->type == GUMBO_NODE_ELEMENT ||
            //           current_node->type == GUMBO_NODE_TEMPLATE);
            //    bool is_closed_body_or_html_tag =
            //        (node_html_tag_is(current_node, GUMBO_TAG_BODY) &&
            //            state->_closed_body_tag) ||
            //        (node_html_tag_is(current_node, GUMBO_TAG_HTML) &&
            //            state->_closed_html_tag);
            //    if ((state->_current_token->type != GUMBO_TOKEN_END_TAG ||
            //            !node_html_tag_is(current_node, state->_current_token->v.end_tag)) &&
            //        !is_closed_body_or_html_tag)
            //    {
            //        current_node->parse_flags |= GUMBO_INSERTION_IMPLICIT_END_TAG;
            //    }
            //    if (!is_closed_body_or_html_tag)
            //    {
            //        record_end_of_element(state->_current_token, &current_node->v.element);
            //    }
            //    return current_node;
            //}
        }
        void append_comment_node()
        {
            throw new TODOImplementException();
            //line:960
            //      static void append_comment_node(
            //          GumboParser* parser, GumboNode* node, const GumboToken* token) {
            //maybe_flush_text_node_buffer(parser);
            //      GumboNode* comment = create_node(parser, GUMBO_NODE_COMMENT);
            //      comment->type = GUMBO_NODE_COMMENT;
            //comment->parse_flags = GUMBO_INSERTION_NORMAL;
            //comment->v.text.text = token->v.text;
            //comment->v.text.original_text = token->original_text;
            //comment->v.text.start_pos = token->position;
            //append_node(parser, node, comment);
            //  }

        }
        void clear_stack_to_table_row_context()
        {
            throw new TODOImplementException();
            //line: 973
            //// http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#clear-the-stack-back-to-a-table-row-context
            //static void clear_stack_to_table_row_context(GumboParser* parser)
            //{
            //    while (!node_tag_in_set(get_current_node(parser),
            //               (gumbo_tagset){ TAG(HTML), TAG(TR), TAG(TEMPLATE)})) {
            //        pop_current_node(parser);
            //    }
            //}
        }

        void clear_stack_to_table_context()
        {
            throw new TODOImplementException();
            //line: 981
            //// http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#clear-the-stack-back-to-a-table-context
            //static void clear_stack_to_table_context(GumboParser* parser)
            //{
            //    while (!node_tag_in_set(get_current_node(parser),
            //               (gumbo_tagset){ TAG(HTML), TAG(TABLE), TAG(TEMPLATE)})) {
            //        pop_current_node(parser);
            //    }
            //}
        }

        void clear_stack_to_table_body_context()
        {
            throw new TODOImplementException();
            //line: 989
            //// http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#clear-the-stack-back-to-a-table-body-context
            //void clear_stack_to_table_body_context(GumboParser* parser)
            //{
            //    while (!node_tag_in_set(get_current_node(parser),
            //               (gumbo_tagset){
            //        TAG(HTML), TAG(TBODY), TAG(TFOOT), TAG(THEAD),
            //         TAG(TEMPLATE)})) {
            //        pop_current_node(parser);
            //    }
            //} 
        }
        void create_element()
        {
            throw new TODOImplementException();
            //line: 998
            //// Creates a parser-inserted element in the HTML namespace and returns it.
            //static GumboNode* create_element(GumboParser* parser, GumboTag tag)
            //{
            //    GumboNode* node = create_node(parser, GUMBO_NODE_ELEMENT);
            //    GumboElement* element = &node->v.element;
            //    gumbo_vector_init(parser, 1, &element->children);
            //    gumbo_vector_init(parser, 0, &element->attributes);
            //    element->tag = tag;
            //    element->tag_namespace = GUMBO_NAMESPACE_HTML;
            //    element->original_tag = kGumboEmptyString;
            //    element->original_end_tag = kGumboEmptyString;
            //    element->start_pos = (parser->_parser_state->_current_token)
            //                             ? parser->_parser_state->_current_token->position
            //                             : kGumboEmptySourcePosition;
            //    element->end_pos = kGumboEmptySourcePosition;
            //    return node;
            //} 
        }
        void create_element_from_token()
        {
            throw new TODOImplementException();
            //line: 1015
            //// Constructs an element from the given start tag token.
            //static GumboNode* create_element_from_token(
            //    GumboParser* parser, GumboToken* token, GumboNamespaceEnum tag_namespace)
            //{
            //    assert(token->type == GUMBO_TOKEN_START_TAG);
            //    GumboTokenStartTag* start_tag = &token->v.start_tag;

            //    GumboNodeType type = (tag_namespace == GUMBO_NAMESPACE_HTML &&
            //                             start_tag->tag == GUMBO_TAG_TEMPLATE)
            //                             ? GUMBO_NODE_TEMPLATE
            //                             : GUMBO_NODE_ELEMENT;

            //    GumboNode* node = create_node(parser, type);
            //    GumboElement* element = &node->v.element;
            //    gumbo_vector_init(parser, 1, &element->children);
            //    element->attributes = start_tag->attributes;
            //    element->tag = start_tag->tag;
            //    element->tag_namespace = tag_namespace;

            //    assert(token->original_text.length >= 2);
            //    assert(token->original_text.data[0] == '<');
            //    assert(token->original_text.data[token->original_text.length - 1] == '>');
            //    element->original_tag = token->original_text;
            //    element->start_pos = token->position;
            //    element->original_end_tag = kGumboEmptyString;
            //    element->end_pos = kGumboEmptySourcePosition;

            //    // The element takes ownership of the attributes from the token, so any
            //    // allocated-memory fields should be nulled out.
            //    start_tag->attributes = kGumboEmptyVector;
            //    return node;
            //}
        }
        void insert_element()
        {
            throw new TODOImplementException();
            //line: 1047
            //// http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#insert-an-html-element
            //static void insert_element(GumboParser* parser, GumboNode* node,
            //    bool is_reconstructing_formatting_elements)
            //{
            //    GumboParserState* state = parser->_parser_state;
            //    // NOTE(jdtang): The text node buffer must always be flushed before inserting
            //    // a node, otherwise we're handling nodes in a different order than the spec
            //    // mandated.  However, one clause of the spec (character tokens in the body)
            //    // requires that we reconstruct the active formatting elements *before* adding
            //    // the character, and reconstructing the active formatting elements may itself
            //    // result in the insertion of new elements (which should be pushed onto the
            //    // stack of open elements before the buffer is flushed).  We solve this (for
            //    // the time being, the spec has been rewritten for <template> and the new
            //    // version may be simpler here) with a boolean flag to this method.
            //    if (!is_reconstructing_formatting_elements)
            //    {
            //        maybe_flush_text_node_buffer(parser);
            //    }
            //    InsertionLocation location = get_appropriate_insertion_location(parser, NULL);
            //    insert_node(parser, node, location);
            //    gumbo_vector_add(parser, (void*)node, &state->_open_elements);
            //}
        }

        void insert_element_from_token()
        {
            throw new TODOImplementException();
            //line: 1070
            //// Convenience method that combines create_element_from_token and
            //// insert_element, inserting the generated element directly into the current
            //// node.  Returns the node inserted.
            //static GumboNode* insert_element_from_token(
            //    GumboParser* parser, GumboToken* token)
            //{
            //    GumboNode* element =
            //        create_element_from_token(parser, token, GUMBO_NAMESPACE_HTML);
            //    insert_element(parser, element, false);
            //    gumbo_debug("Inserting <%s> element (@%x) from token.\n",
            //        gumbo_normalized_tagname(element->v.element.tag), element);
            //    return element;
            //}
        }
        void insert_element_of_tag_type()
        {
            throw new TODOImplementException();
            //line: 1083
            //// Convenience method that combines create_element and insert_element, inserting
            //// a parser-generated element of a specific tag type.  Returns the node
            //// inserted.
            //static GumboNode* insert_element_of_tag_type(
            //    GumboParser* parser, GumboTag tag, GumboParseFlags reason)
            //{
            //    GumboNode* element = create_element(parser, tag);
            //    element->parse_flags |= GUMBO_INSERTION_BY_PARSER | reason;
            //    insert_element(parser, element, false);
            //    gumbo_debug("Inserting %s element (@%x) from tag type.\n",
            //        gumbo_normalized_tagname(tag), element);
            //    return element;
            //}
        }
        void insert_foreign_element()
        {
            throw new TODOImplementException();
            //line: 1095
            //// Convenience method for creating foreign namespaced element.  Returns the node
            //// inserted.
            //static GumboNode* insert_foreign_element(
            //    GumboParser* parser, GumboToken* token, GumboNamespaceEnum tag_namespace)
            //{
            //    assert(token->type == GUMBO_TOKEN_START_TAG);
            //    GumboNode* element = create_element_from_token(parser, token, tag_namespace);
            //    insert_element(parser, element, false);
            //    if (token_has_attribute(token, "xmlns") &&
            //        !attribute_matches_case_sensitive(&token->v.start_tag.attributes, "xmlns",
            //            kLegalXmlns[tag_namespace]))
            //    {
            //        // TODO(jdtang): Since there're multiple possible error codes here, we
            //        // eventually need reason codes to differentiate them.
            //        parser_add_parse_error(parser, token);
            //    }
            //    if (token_has_attribute(token, "xmlns:xlink") &&
            //        !attribute_matches_case_sensitive(&token->v.start_tag.attributes,
            //            "xmlns:xlink", "http://www.w3.org/1999/xlink"))
            //    {
            //        parser_add_parse_error(parser, token);
            //    }
            //    return element;
            //} 
        }
        void insert_text_token()
        {
            throw new TODOImplementException();
            //line: 1115
            //static void insert_text_token(GumboParser* parser, GumboToken* token)
            //{
            //    assert(token->type == GUMBO_TOKEN_WHITESPACE ||
            //           token->type == GUMBO_TOKEN_CHARACTER ||
            //           token->type == GUMBO_TOKEN_NULL || token->type == GUMBO_TOKEN_CDATA);
            //    TextNodeBufferState* buffer_state = &parser->_parser_state->_text_node;
            //    if (buffer_state->_buffer.length == 0)
            //    {
            //        // Initialize position fields.
            //        buffer_state->_start_original_text = token->original_text.data;
            //        buffer_state->_start_position = token->position;
            //    }
            //    gumbo_string_buffer_append_codepoint(
            //        parser, token->v.character, &buffer_state->_buffer);
            //    if (token->type == GUMBO_TOKEN_CHARACTER)
            //    {
            //        buffer_state->_type = GUMBO_NODE_TEXT;
            //    }
            //    else if (token->type == GUMBO_TOKEN_CDATA)
            //    {
            //        buffer_state->_type = GUMBO_NODE_CDATA;
            //    }
            //    gumbo_debug("Inserting text token '%c'.\n", token->v.character);
            //} 
        }

        void run_generic_parsing_algorithm()
        {
            throw new TODOImplementException();
            //line: 1136
            //// http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#generic-rcdata-element-parsing-algorithm
            //static void run_generic_parsing_algorithm(
            //    GumboParser* parser, GumboToken* token, GumboTokenizerEnum lexer_state)
            //{
            //    insert_element_from_token(parser, token);
            //    gumbo_tokenizer_set_state(parser, lexer_state);
            //    parser->_parser_state->_original_insertion_mode =
            //        parser->_parser_state->_insertion_mode;
            //    parser->_parser_state->_insertion_mode = GUMBO_INSERTION_MODE_TEXT;
            //} 
        }

        void acknowledge_self_closing_tag()
        {
            throw new TODOImplementException();
            //line: 1145
            //static void acknowledge_self_closing_tag(GumboParser* parser)
            //{
            //    parser->_parser_state->_self_closing_flag_acknowledged = true;
            //}
        }
        bool find_last_anchor_index()
        {
            throw new TODOImplementException();
            //line: 1151
            //// Returns true if there's an anchor tag in the list of active formatting
            //// elements, and fills in its index if so.
            //static bool find_last_anchor_index(GumboParser* parser, int* anchor_index)
            //{
            //    GumboVector* elements = &parser->_parser_state->_active_formatting_elements;
            //    for (int i = elements->length; --i >= 0;)
            //    {
            //        GumboNode* node = elements->data[i];
            //        if (node == &kActiveFormattingScopeMarker)
            //        {
            //            return false;
            //        }
            //        if (node_html_tag_is(node, GUMBO_TAG_A))
            //        {
            //            *anchor_index = i;
            //            return true;
            //        }
            //    }
            //    return false;
            //} 
        }
        int count_formatting_elements_of_tag()
        {
            throw new TODOImplementException();
            //line:  1170
            //        // Counts the number of open formatting elements in the list of active
            //        // formatting elements (after the last active scope marker) that have a specific
            //        // tag.  If this is > 0, then earliest_matching_index will be filled in with the
            //        // index of the first such element.
            //static int count_formatting_elements_of_tag(GumboParser* parser,
            //    const GumboNode* desired_node, int* earliest_matching_index) {
            //  const GumboElement* desired_element = &desired_node->v.element;
            //        GumboVector* elements = &parser->_parser_state->_active_formatting_elements;
            //        int num_identical_elements = 0;
            //  for (int i = elements->length; --i >= 0;) {
            //    GumboNode* node = elements->data[i];
            //    if (node == &kActiveFormattingScopeMarker) {
            //      break;
            //    }
            //    assert(node->type == GUMBO_NODE_ELEMENT);
            //    if (node_qualified_tag_is(
            //            node, desired_element->tag_namespace, desired_element->tag) &&
            //        all_attributes_match(
            //            &node->v.element.attributes, &desired_element->attributes)) {
            //      num_identical_elements++;
            //      *earliest_matching_index = i;
            //    }
            //}
            //  return num_identical_elements;
            //}
        }
        void add_formatting_element()
        {
            throw new TODOImplementException();
            //line: 1193 
            //        // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#reconstruct-the-active-formatting-elements
            //        static void add_formatting_element(GumboParser* parser, const GumboNode* node) {
            //  assert(node == &kActiveFormattingScopeMarker ||
            //         node->type == GUMBO_NODE_ELEMENT);
            //        GumboVector* elements = &parser->_parser_state->_active_formatting_elements;
            //  if (node == &kActiveFormattingScopeMarker) {
            //    gumbo_debug("Adding a scope marker.\n");
            //    } else {
            //    gumbo_debug("Adding a formatting element.\n");
            //}

            //// Hunt for identical elements.
            //int earliest_identical_element = elements->length;
            //int num_identical_elements = count_formatting_elements_of_tag(
            //    parser, node, &earliest_identical_element);

            //  // Noah's Ark clause: if there're at least 3, remove the earliest.
            //  if (num_identical_elements >= 3) {
            //    gumbo_debug("Noah's ark clause: removing element at %d.\n",
            //        earliest_identical_element);
            //    gumbo_vector_remove_at(parser, earliest_identical_element, elements);
            //  }

            //  gumbo_vector_add(parser, (void*) node, elements);
            //}
        }

        bool is_open_element()
        {
            //line: 1218
            throw new TODOImplementException();
            //        static bool is_open_element(GumboParser* parser, const GumboNode* node) {
            //  GumboVector* open_elements = &parser->_parser_state->_open_elements;
            //  for (unsigned int i = 0; i<open_elements->length; ++i) {
            //    if (open_elements->data[i] == node) {
            //      return true;
            //    }
            //  }
            //  return false;
            //}
        }
        void clone_node()
        {
            throw new TODOImplementException();
            //line: 1231
            //// Clones attributes, tags, etc. of a node, but does not copy the content.  The
            //// clone shares no structure with the original node: all owned strings and
            //// values are fresh copies.
            //GumboNode* clone_node(
            //    GumboParser* parser, GumboNode* node, GumboParseFlags reason)
            //{
            //    assert(node->type == GUMBO_NODE_ELEMENT || node->type == GUMBO_NODE_TEMPLATE);
            //    GumboNode* new_node = gumbo_parser_allocate(parser, sizeof(GumboNode));
            //    *new_node = *node;
            //    new_node->parent = NULL;
            //    new_node->index_within_parent = -1;
            //    // Clear the GUMBO_INSERTION_IMPLICIT_END_TAG flag, as the cloned node may
            //    // have a separate end tag.
            //    new_node->parse_flags &= ~GUMBO_INSERTION_IMPLICIT_END_TAG;
            //    new_node->parse_flags |= reason | GUMBO_INSERTION_BY_PARSER;
            //    GumboElement* element = &new_node->v.element;
            //    gumbo_vector_init(parser, 1, &element->children);

            //    const GumboVector* old_attributes = &node->v.element.attributes;
            //    gumbo_vector_init(parser, old_attributes->length, &element->attributes);
            //    for (unsigned int i = 0; i < old_attributes->length; ++i)
            //    {
            //        const GumboAttribute* old_attr = old_attributes->data[i];
            //        GumboAttribute* attr =
            //            gumbo_parser_allocate(parser, sizeof(GumboAttribute));
            //        *attr = *old_attr;
            //        attr->name = gumbo_copy_stringz(parser, old_attr->name);
            //        attr->value = gumbo_copy_stringz(parser, old_attr->value);
            //        gumbo_vector_add(parser, attr, &element->attributes);
            //    }
            //    return new_node;
            //} 
        }
        void reconstruct_active_formatting_elements()
        {
            throw new TODOImplementException();
            //line: 1263
            //// "Reconstruct active formatting elements" part of the spec.
            //// This implementation is based on the html5lib translation from the mess of
            //// GOTOs in the spec to reasonably structured programming.
            //// http://code.google.com/p/html5lib/source/browse/python/html5lib/treebuilders/_base.py
            //static void reconstruct_active_formatting_elements(GumboParser* parser)
            //{
            //    GumboVector* elements = &parser->_parser_state->_active_formatting_elements;
            //    // Step 1
            //    if (elements->length == 0)
            //    {
            //        return;
            //    }

            //    // Step 2 & 3
            //    unsigned int i = elements->length - 1;
            //    GumboNode* element = elements->data[i];
            //    if (element == &kActiveFormattingScopeMarker ||
            //        is_open_element(parser, element))
            //    {
            //        return;
            //    }

            //    // Step 6
            //    do
            //    {
            //        if (i == 0)
            //        {
            //            // Step 4
            //            i = -1;  // Incremented to 0 below.
            //            break;
            //        }
            //        // Step 5
            //        element = elements->data[--i];
            //    } while (element != &kActiveFormattingScopeMarker &&
            //             !is_open_element(parser, element));

            //    ++i;
            //    gumbo_debug("Reconstructing elements from %d on %s parent.\n", i,
            //        gumbo_normalized_tagname(get_current_node(parser)->v.element.tag));
            //    for (; i < elements->length; ++i)
            //    {
            //        // Step 7 & 8.
            //        assert(elements->length > 0);
            //        assert(i < elements->length);
            //        element = elements->data[i];
            //        assert(element != &kActiveFormattingScopeMarker);
            //        GumboNode* clone = clone_node(
            //            parser, element, GUMBO_INSERTION_RECONSTRUCTED_FORMATTING_ELEMENT);
            //        // Step 9.
            //        InsertionLocation location =
            //            get_appropriate_insertion_location(parser, NULL);
            //        insert_node(parser, clone, location);
            //        gumbo_vector_add(
            //            parser, (void*)clone, &parser->_parser_state->_open_elements);

            //        // Step 10.
            //        elements->data[i] = clone;
            //        gumbo_debug("Reconstructed %s element at %d.\n",
            //            gumbo_normalized_tagname(clone->v.element.tag), i);
            //    }
            //}
        }
        void clear_active_formatting_elements()
        {
            throw new TODOImplementException();
            //line: 1315
            //static void clear_active_formatting_elements(GumboParser* parser)
            //{
            //    GumboVector* elements = &parser->_parser_state->_active_formatting_elements;
            //    int num_elements_cleared = 0;
            //    const GumboNode* node;
            //    do
            //    {
            //        node = gumbo_vector_pop(parser, elements);
            //        ++num_elements_cleared;
            //    } while (node && node != &kActiveFormattingScopeMarker);
            //    gumbo_debug("Cleared %d elements from active formatting list.\n",
            //        num_elements_cleared);
            //}
        }
        void compute_quirks_mode()
        {
            throw new TODOImplementException();
            //line: 1328
            //        // http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#the-initial-insertion-mode
            //        static GumboQuirksModeEnum compute_quirks_mode(
            //    const GumboTokenDocType* doctype) {
            //  if (doctype->force_quirks || strcmp(doctype->name, kDoctypeHtml.data) ||
            //      is_in_static_list(
            //          doctype->public_identifier, kQuirksModePublicIdPrefixes, false) ||
            //      is_in_static_list(
            //          doctype->public_identifier, kQuirksModePublicIdExactMatches, true) ||
            //      is_in_static_list(
            //          doctype->system_identifier, kQuirksModeSystemIdExactMatches, true) ||
            //      (is_in_static_list(doctype->public_identifier,
            //           kLimitedQuirksRequiresSystemIdPublicIdPrefixes, false) &&
            //          !doctype->has_system_identifier)) {
            //    return GUMBO_DOCTYPE_QUIRKS;
            //  } else if (is_in_static_list(doctype->public_identifier,
            //                 kLimitedQuirksPublicIdPrefixes, false) ||
            //             (is_in_static_list(doctype->public_identifier,
            //                  kLimitedQuirksRequiresSystemIdPublicIdPrefixes, false) &&
            //                 doctype->has_system_identifier)) {
            //    return GUMBO_DOCTYPE_LIMITED_QUIRKS;
            //  }
            //  return GUMBO_DOCTYPE_NO_QUIRKS;
            //}
        }


        bool has_an_element_in_specific_scope()
        {
            throw new TODOImplementException();
            //line:  1362
            //        // The following functions are all defined by the "has an element in __ scope"
            //        // sections of the HTML5 spec:
            //        // http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#has-an-element-in-the-specific-scope
            //        // The basic idea behind them is that they check for an element of the given
            //        // qualified name, contained within a scope formed by a set of other qualified
            //        // names.  For example, "has an element in list scope" looks for an element of
            //        // the given qualified name within the nearest enclosing <ol> or <ul>, along
            //        // with a bunch of generic element types that serve to "firewall" their content
            //        // from the rest of the document. Note that because of the way the spec is
            //        // written,
            //        // all elements are expected to be in the HTML namespace
            //        static bool has_an_element_in_specific_scope(GumboParser* parser,
            //            int expected_size, const GumboTag* expected, bool negate,
            //    const gumbo_tagset tags) {
            //  GumboVector* open_elements = &parser->_parser_state->_open_elements;
            //  for (int i = open_elements->length; --i >= 0;) {
            //    const GumboNode* node = open_elements->data[i];
            //    if (node->type != GUMBO_NODE_ELEMENT && node->type != GUMBO_NODE_TEMPLATE)
            //      continue;

            //    GumboTag node_tag = node->v.element.tag;
            //        GumboNamespaceEnum node_ns = node->v.element.tag_namespace;
            //    for (int j = 0; j<expected_size; ++j) {
            //      if (node_tag == expected[j] && node_ns == GUMBO_NAMESPACE_HTML)
            //        return true;
            //    }

            //    bool found = TAGSET_INCLUDES(tags, node_ns, node_tag);
            //    if (negate != found) return false;
            //  }
            //  return false;
            //}

        }
        bool has_open_element()
        {
            throw new TODOImplementException();
            //line: 1385
            //// Checks for the presence of an open element of the specified tag type.
            //static bool has_open_element(GumboParser* parser, GumboTag tag)
            //{
            //    return has_an_element_in_specific_scope(
            //        parser, 1, &tag, false, (gumbo_tagset){ TAG(HTML)});
            //}
        }
        bool has_an_element_in_scope()
        {
            throw new TODOImplementException();
            //line: 1391
            //// http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#has-an-element-in-scope
            //static bool has_an_element_in_scope(GumboParser* parser, GumboTag tag)
            //{
            //    return has_an_element_in_specific_scope(parser, 1, &tag, false,
            //        (gumbo_tagset){
            //        TAG(APPLET), TAG(CAPTION), TAG(HTML), TAG(TABLE), TAG(TD),
            //  TAG(TH), TAG(MARQUEE), TAG(OBJECT), TAG(TEMPLATE), TAG_MATHML(MI),
            //  TAG_MATHML(MO), TAG_MATHML(MN), TAG_MATHML(MS), TAG_MATHML(MTEXT),
            //  TAG_MATHML(ANNOTATION_XML), TAG_SVG(FOREIGNOBJECT), TAG_SVG(DESC),
            //  TAG_SVG(TITLE)});
            //}
        }
        bool has_node_in_scope()
        {

            throw new TODOImplementException();
            //line: 1406
            //        // Like "has an element in scope", but for the specific case of looking for a
            //        // unique target node, not for any node with a given tag name.  This duplicates
            //        // much of the algorithm from has_an_element_in_specific_scope because the
            //        // predicate is different when checking for an exact node, and it's easier &
            //        // faster just to duplicate the code for this one case than to try and
            //        // parameterize it.
            //static bool has_node_in_scope(GumboParser* parser, const GumboNode* node) {
            //  GumboVector* open_elements = &parser->_parser_state->_open_elements;
            //  for (int i = open_elements->length; --i >= 0;) {
            //    const GumboNode* current = open_elements->data[i];
            //    if (current == node) {
            //      return true;
            //    }
            //    if (current->type != GUMBO_NODE_ELEMENT &&
            //        current->type != GUMBO_NODE_TEMPLATE) {
            //      continue;
            //    }
            //    if (node_tag_in_set(current,
            //            (gumbo_tagset){TAG(APPLET), TAG(CAPTION), TAG(HTML), TAG(TABLE),
            //                TAG(TD), TAG(TH), TAG(MARQUEE), TAG(OBJECT), TAG(TEMPLATE),
            //                TAG_MATHML(MI), TAG_MATHML(MO), TAG_MATHML(MN), TAG_MATHML(MS),
            //                TAG_MATHML(MTEXT), TAG_MATHML(ANNOTATION_XML),
            //                TAG_SVG(FOREIGNOBJECT), TAG_SVG(DESC), TAG_SVG(TITLE)})) {
            //      return false;
            //    }
            //  }
            //  assert(false);
            //  return false;
            //}
        }

        bool has_an_element_in_scope_with_tagname()
        {
            throw new TODOImplementException();
            //line: 1432
            //        // Like has_an_element_in_scope, but restricts the expected qualified name to a
            //        // range of possible qualified names instead of just a single one.
            // static bool has_an_element_in_scope_with_tagname(
            //            GumboParser* parser, int expected_len, const GumboTag expected[]) {
            //  return has_an_element_in_specific_scope(parser, expected_len, expected, false,
            //      (gumbo_tagset){
            //            TAG(APPLET), TAG(CAPTION), TAG(HTML), TAG(TABLE), TAG(TD),
            //          TAG(TH), TAG(MARQUEE), TAG(OBJECT), TAG(TEMPLATE), TAG_MATHML(MI),
            //          TAG_MATHML(MO), TAG_MATHML(MN), TAG_MATHML(MS), TAG_MATHML(MTEXT),
            //          TAG_MATHML(ANNOTATION_XML), TAG_SVG(FOREIGNOBJECT), TAG_SVG(DESC),
            //          TAG_SVG(TITLE)});
            //}
        }

        bool has_an_element_in_list_scope()
        {
            throw new TODOImplementException();
            //line: 1443
            //// http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#has-an-element-in-list-item-scope
            //static bool has_an_element_in_list_scope(GumboParser* parser, GumboTag tag)
            //{
            //    return has_an_element_in_specific_scope(parser, 1, &tag, false,
            //        (gumbo_tagset){
            //        TAG(APPLET), TAG(CAPTION), TAG(HTML), TAG(TABLE), TAG(TD),
            //  TAG(TH), TAG(MARQUEE), TAG(OBJECT), TAG(TEMPLATE), TAG_MATHML(MI),
            //  TAG_MATHML(MO), TAG_MATHML(MN), TAG_MATHML(MS), TAG_MATHML(MTEXT),
            //  TAG_MATHML(ANNOTATION_XML), TAG_SVG(FOREIGNOBJECT), TAG_SVG(DESC),
            //  TAG_SVG(TITLE), TAG(OL), TAG(UL)});
            //}
        }

        bool has_an_element_in_button_scope()
        {
            throw new TODOImplementException();
            //line: 1453
            //// http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#has-an-element-in-button-scope
            //static bool has_an_element_in_button_scope(GumboParser* parser, GumboTag tag)
            //{
            //    return has_an_element_in_specific_scope(parser, 1, &tag, false,
            //        (gumbo_tagset){
            //        TAG(APPLET), TAG(CAPTION), TAG(HTML), TAG(TABLE), TAG(TD),
            //  TAG(TH), TAG(MARQUEE), TAG(OBJECT), TAG(TEMPLATE), TAG_MATHML(MI),
            //  TAG_MATHML(MO), TAG_MATHML(MN), TAG_MATHML(MS), TAG_MATHML(MTEXT),
            //  TAG_MATHML(ANNOTATION_XML), TAG_SVG(FOREIGNOBJECT), TAG_SVG(DESC),
            //  TAG_SVG(TITLE), TAG(BUTTON)});
            //}
        }

        bool has_an_element_in_table_scope()
        {
            throw new TODOImplementException();
            //line: 1463
            //// http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#has-an-element-in-table-scope
            //static bool has_an_element_in_table_scope(GumboParser* parser, GumboTag tag)
            //{
            //    return has_an_element_in_specific_scope(parser, 1, &tag, false,
            //        (gumbo_tagset){ TAG(HTML), TAG(TABLE), TAG(TEMPLATE)});
            //} 
        }

        bool has_an_element_in_select_scope()
        {
            throw new TODOImplementException();
            //line: 1469
            //    // http://www.whatwg.org/specs/web-apps/current-work/multipage/parsing.html#has-an-element-in-select-scope
            //    static bool has_an_element_in_select_scope(GumboParser* parser, GumboTag tag)
            //    {
            //        return has_an_element_in_specific_scope(
            //            parser, 1, &tag, true, (gumbo_tagset){ TAG(OPTGROUP), TAG(OPTION)});
            //}
        }
        void generate_implied_end_tags()
        {
            throw new TODOImplementException();
            //line: 1477
            //    // http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#generate-implied-end-tags
            //    // "exception" is the "element to exclude from the process" listed in the spec.
            //    // Pass GUMBO_TAG_LAST to not exclude any of them.
            //    static void generate_implied_end_tags(GumboParser* parser, GumboTag exception)
            //    {
            //        for (; node_tag_in_set(get_current_node(parser),
            //                   (gumbo_tagset){
            //            TAG(DD), TAG(DT), TAG(LI), TAG(OPTION),
            //             TAG(OPTGROUP), TAG(P), TAG(RP), TAG(RB), TAG(RT), TAG(RTC)}) &&
            //     !node_html_tag_is(get_current_node(parser), exception);
            //        pop_current_node(parser))
            //;
            //    } 
        }
        void generate_all_implied_end_tags_thoroughly()
        {
            throw new TODOImplementException();
            //line: 1488
            //    // This is the "generate all implied end tags thoroughly" clause of the spec.
            //    // https://html.spec.whatwg.org/multipage/syntax.html#closing-elements-that-have-implied-end-tags
            //    static void generate_all_implied_end_tags_thoroughly(GumboParser* parser)
            //    {
            //        for (
            //            ; node_tag_in_set(get_current_node(parser),
            //                (gumbo_tagset){
            //            TAG(CAPTION), TAG(COLGROUP), TAG(DD), TAG(DT), TAG(LI),
            //          TAG(OPTION), TAG(OPTGROUP), TAG(P), TAG(RP), TAG(RT), TAG(RTC),
            //          TAG(TBODY), TAG(TD), TAG(TFOOT), TAG(TH), TAG(HEAD), TAG(TR)});
            //        pop_current_node(parser))
            //;
            //    } 
        }
        bool close_table()
        {
            throw new TODOImplementException();
            //line: 1502
            //// This factors out the clauses relating to "act as if an end tag token with tag
            //// name "table" had been seen.  Returns true if there's a table element in table
            //// scope which was successfully closed, false if not and the token should be
            //// ignored.  Does not add parse errors; callers should handle that.
            //static bool close_table(GumboParser* parser)
            //{
            //    if (!has_an_element_in_table_scope(parser, GUMBO_TAG_TABLE))
            //    {
            //        return false;
            //    }

            //    GumboNode* node = pop_current_node(parser);
            //    while (!node_html_tag_is(node, GUMBO_TAG_TABLE))
            //    {
            //        node = pop_current_node(parser);
            //    }
            //    reset_insertion_mode_appropriately(parser);
            //    return true;
            //}
        }
        bool close_table_cell()
        {
            throw new TODOImplementException();
            //line: 1517
            //        // This factors out the clauses relating to "act as if an end tag token with tag
            //        // name `cell_tag` had been seen".
            //        static bool close_table_cell(
            //            GumboParser* parser, const GumboToken* token, GumboTag cell_tag) {
            //  bool result = true;
            //  generate_implied_end_tags(parser, GUMBO_TAG_LAST);
            //        const GumboNode* node = get_current_node(parser);
            //  if (!node_html_tag_is(node, cell_tag)) {
            //    parser_add_parse_error(parser, token);
            //        result = false;
            //  }
            //  do {
            //    node = pop_current_node(parser);
            //} while (!node_html_tag_is(node, cell_tag));

            //  clear_active_formatting_elements(parser);
            //  set_insertion_mode(parser, GUMBO_INSERTION_MODE_IN_ROW);
            //  return result;
            //}
        }
        bool close_current_cell()
        {
            throw new TODOImplementException();
            //line: 1537
            //        // http://www.whatwg.org/specs/web-apps/current-work/complete/tokenization.html#close-the-cell
            //        // This holds the logic to determine whether we should close a <td> or a <th>.
            //        static bool close_current_cell(GumboParser* parser, const GumboToken* token) {
            //  if (has_an_element_in_table_scope(parser, GUMBO_TAG_TD)) {
            //    assert(!has_an_element_in_table_scope(parser, GUMBO_TAG_TH));
            //    return close_table_cell(parser, token, GUMBO_TAG_TD);
            //    } else {
            //    assert(has_an_element_in_table_scope(parser, GUMBO_TAG_TH));
            //    return close_table_cell(parser, token, GUMBO_TAG_TH);
            //}
            //} 
        }
        void close_current_select()
        {
            throw new TODOImplementException();
            //line: 1551
            //// This factors out the "act as if an end tag of tag name 'select' had been
            //// seen" clause of the spec, since it's referenced in several places.  It pops
            //// all nodes from the stack until the current <select> has been closed, then
            //// resets the insertion mode appropriately.
            //static void close_current_select(GumboParser* parser)
            //{
            //    GumboNode* node = pop_current_node(parser);
            //    while (!node_html_tag_is(node, GUMBO_TAG_SELECT))
            //    {
            //        node = pop_current_node(parser);
            //    }
            //    reset_insertion_mode_appropriately(parser);
            //}


        }
        bool is_special_node()
        {
            throw new TODOImplementException();
            //line: 1551
            //        // The list of nodes in the "special" category:
            //        // http://www.whatwg.org/specs/web-apps/current-work/complete/parsing.html#special
            //        static bool is_special_node(const GumboNode* node) {
            //  assert(node->type == GUMBO_NODE_ELEMENT || node->type == GUMBO_NODE_TEMPLATE);
            //  return node_tag_in_set(node,
            //      (gumbo_tagset){
            //            TAG(ADDRESS), TAG(APPLET), TAG(AREA), TAG(ARTICLE),
            //          TAG(ASIDE), TAG(BASE), TAG(BASEFONT), TAG(BGSOUND), TAG(BLOCKQUOTE),
            //          TAG(BODY), TAG(BR), TAG(BUTTON), TAG(CAPTION), TAG(CENTER), TAG(COL),
            //          TAG(COLGROUP), TAG(MENUITEM), TAG(DD), TAG(DETAILS), TAG(DIR),
            //          TAG(DIV), TAG(DL), TAG(DT), TAG(EMBED), TAG(FIELDSET),
            //          TAG(FIGCAPTION), TAG(FIGURE), TAG(FOOTER), TAG(FORM), TAG(FRAME),
            //          TAG(FRAMESET), TAG(H1), TAG(H2), TAG(H3), TAG(H4), TAG(H5), TAG(H6),
            //          TAG(HEAD), TAG(HEADER), TAG(HGROUP), TAG(HR), TAG(HTML), TAG(IFRAME),
            //          TAG(IMG), TAG(INPUT), TAG(ISINDEX), TAG(LI), TAG(LINK), TAG(LISTING),
            //          TAG(MARQUEE), TAG(MENU), TAG(META), TAG(NAV), TAG(NOEMBED),
            //          TAG(NOFRAMES), TAG(NOSCRIPT), TAG(OBJECT), TAG(OL), TAG(P),
            //          TAG(PARAM), TAG(PLAINTEXT), TAG(PRE), TAG(SCRIPT), TAG(SECTION),
            //          TAG(SELECT), TAG(STYLE), TAG(SUMMARY), TAG(TABLE), TAG(TBODY),
            //          TAG(TD), TAG(TEMPLATE), TAG(TEXTAREA), TAG(TFOOT), TAG(TH),
            //          TAG(THEAD), TAG(TITLE), TAG(TR), TAG(UL), TAG(WBR), TAG(XMP),

            //          TAG_MATHML(MI), TAG_MATHML(MO), TAG_MATHML(MN), TAG_MATHML(MS),
            //          TAG_MATHML(MTEXT), TAG_MATHML(ANNOTATION_XML),

            //          TAG_SVG(FOREIGNOBJECT), TAG_SVG(DESC)});
            //}
        }
        bool implicitly_close_tags()
        {
            //line: 1591
            throw new TODOImplementException();
            //// Implicitly closes currently open elements until it reaches an element with
            //// the
            //// specified qualified name.  If the elements closed are in the set handled by
            //// generate_implied_end_tags, this is normal operation and this function returns
            //// true.  Otherwise, a parse error is recorded and this function returns false.
            //static bool implicitly_close_tags(GumboParser* parser, GumboToken* token,
            //    GumboNamespaceEnum target_ns, GumboTag target)
            //{
            //    bool result = true;
            //    generate_implied_end_tags(parser, target);
            //    if (!node_qualified_tag_is(get_current_node(parser), target_ns, target))
            //    {
            //        parser_add_parse_error(parser, token);
            //        while (
            //            !node_qualified_tag_is(get_current_node(parser), target_ns, target))
            //        {
            //            pop_current_node(parser);
            //        }
            //        result = false;
            //    }
            //    assert(node_qualified_tag_is(get_current_node(parser), target_ns, target));
            //    pop_current_node(parser);
            //    return result;
            //}
        }
        bool maybe_implicitly_close_p_tag()
        {
            //line: 1612
            throw new TODOImplementException();
            //// If the stack of open elements has a <p> tag in button scope, this acts as if
            //// a </p> tag was encountered, implicitly closing tags.  Returns false if a
            //// parse error occurs.  This is a convenience function because this particular
            //// clause appears several times in the spec.
            //static bool maybe_implicitly_close_p_tag(
            //    GumboParser* parser, GumboToken* token)
            //{
            //    if (has_an_element_in_button_scope(parser, GUMBO_TAG_P))
            //    {
            //        return implicitly_close_tags(
            //            parser, token, GUMBO_NAMESPACE_HTML, GUMBO_TAG_P);
            //    }
            //    return true;
            //}
        }
        void maybe_implicitly_close_list_tag()
        {
            //line: 1623
            throw new TODOImplementException();

            //    // Convenience function to encapsulate the logic for closing <li> or <dd>/<dt>
            //    // tags.  Pass true to is_li for handling <li> tags, false for <dd> and <dt>.
            //    static void maybe_implicitly_close_list_tag(
            //        GumboParser* parser, GumboToken* token, bool is_li)
            //    {
            //        GumboParserState* state = parser->_parser_state;
            //        state->_frameset_ok = false;
            //        for (int i = state->_open_elements.length; --i >= 0;)
            //        {
            //            const GumboNode* node = state->_open_elements.data[i];
            //            bool is_list_tag =
            //                is_li ? node_html_tag_is(node, GUMBO_TAG_LI)
            //                      : node_tag_in_set(node, (gumbo_tagset){ TAG(DD), TAG(DT)});
            //        if (is_list_tag)
            //        {
            //            implicitly_close_tags(
            //                parser, token, node->v.element.tag_namespace, node->v.element.tag);
            //            return;
            //        }
            //        if (is_special_node(node) &&
            //            !node_tag_in_set(
            //                node, (gumbo_tagset){ TAG(ADDRESS), TAG(DIV), TAG(P)})) {
            //            return;
            //        }
            //    }
            //}
        }

    }

    struct InsertionLocation
    {
        //line: 774
        // This represents a place to insert a node, consisting of a target parent and a
        // child index within that parent.  If the node should be inserted at the end of
        // the parent's child, index will be -1.
        public GumboNode target;
        int index;
    }
}
