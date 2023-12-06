import {useEffect, useState} from 'react';
import {FlatList, TouchableOpacity, View, Text,ActivityIndicator} from 'react-native';
import {fetchGetCommentsPostById} from '../../../services/APIService';
import {showMessage} from 'react-native-flash-message';
import CommentsDetailItem from '../CommentsDetailItem/CommentsDetailItem';

function CommentsDetail({route}) {
  const {postId} = route.params;

  const initialStates = {
    fetchResult: {
      loading: true,
      error: null,
    },
    comments: {
      main: {
        metaData: {},
        data: [],
      },
      sub: [
        {
          parentCommentId: '',
          metaData: {},
          data: [],
        },
      ],
    },
  };

  const [comments, setComments] = useState(initialStates.comments);
  const [fetchResult, setFetchResult] = useState(initialStates.fetchResult);

  const renderCommentsDetailItem = ({item: comment}) => {
    return <CommentsDetailItem comment={comment} parentCommentId={comment.id} />;
  };

  const handleFetchGetCommentsByPostId = (postId, pageNumber) => {
    fetchGetCommentsPostById(postId, pageNumber)
      .then(res => {
        const {data, metaData} = res.data;

        setFetchResult({
          ...fetchResult,
          loading: false,
        });

        setComments({
          ...comments,
          main: {
            data: [...comments.main.data, ...data],
            metaData: metaData,
          },
        });
      })
      .catch(error => {
        const message =
          'An internal server error occurred. Please try again later.';
        setFetchResult({
          loading: false,
          error: message,
        });
        showMessage({
          type: 'danger',
          message,
        });
      });
  };

  useEffect(() => {
    handleFetchGetCommentsByPostId(postId);
  }, [postId]);

  if (fetchResult.loading) {
    return (
      <View style={{flex: 1}}>
        <ActivityIndicator
          size={40}
          color={"darkred"}
        />
      </View>
    );
  }

  return (
    <View style={{gap: 10, padding: 10}}>
      <FlatList
        data={comments.main.data}
        renderItem={renderCommentsDetailItem}
        keyExtractor={item => item.id}
        ListFooterComponent={
          comments.main.metaData.totalPages !==
            comments.main.metaData.currentPage && (
            <View>
              <TouchableOpacity
                onPress={() =>
                  handleFetchGetCommentsByPostId(
                    postId,
                    comments.main.metaData.currentPage + 1,
                  )
                }>
                <Text style={{color: 'gray'}}>
                  Show comments (
                  {comments.main.metaData.totalPages === 1
                    ? comments.main.metaData.totalRecords
                    : comments.main.metaData.totalRecords -
                      comments.main.metaData.pageSize *
                        comments.main.metaData.currentPage}
                  )
                </Text>
              </TouchableOpacity>
            </View>
          )
        }
      />
    </View>
  );
}

export default CommentsDetail;
