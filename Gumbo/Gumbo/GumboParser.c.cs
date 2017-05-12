
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
            throw new ToImplementException();
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
            throw new ToImplementException();
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


            throw new ToImplementException();
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
            throw new ToImplementException();
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
            throw new ToImplementException();
        }


        bool node_html_tag_is()
        {
            //line: 726
            //      // Like node_tag_in, but for the single-tag case in the HTML namespace
            //      static bool node_html_tag_is(const GumboNode* node, GumboTag tag) {
            //return node_qualified_tag_is(node, GUMBO_NAMESPACE_HTML, tag);
            //  }
            throw new ToImplementException();
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
            throw new ToImplementException();
        }
        void pop_template_insertion_mode()
        {
            //line: 735
            //static void pop_template_insertion_mode(GumboParser* parser)
            //{
            //    gumbo_vector_pop(parser, &parser->_parser_state->_template_insertion_modes);
            //}
            throw new ToImplementException();
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

            throw new ToImplementException();
        }
        static bool is_html_integration_point()
        {
            throw new ToImplementException();
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
            throw new ToImplementException();
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
            throw new ToImplementException();
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
            throw new ToImplementException();
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

            throw new ToImplementException();
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
