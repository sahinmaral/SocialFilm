import {useState, useEffect, useMemo} from 'react';
import {
  StyleSheet,
  TouchableOpacity,
  View,
  FlatList,
  Text,
  Image,
} from 'react-native';
import {fetchGetSubCommentsByParentCommentId} from '../../../services/APIService';

function CommentsDetailItem({comment, parentCommentId}) {
  const [showSubComments, setShowSubComments] = useState(false);

  const [subComments, setSubComments] = useState(null);

  const renderCommentsDetailItem = ({item: subComment}) => {
    return <CommentsDetailItem comment={subComment} />;
  };

  const handleFetchGetSubCommentsByParentCommentId = () => {
    fetchGetSubCommentsByParentCommentId(
      parentCommentId,
      subComments.metaData.currentPage + 1,
    )
      .then(res => {
        const {data, metaData} = res.data;

        setSubComments({
          data: [...subComments.data, ...data],
          metaData: metaData,
        });

        // setFetchResult({
        //   ...fetchResult,
        //   loading: false,
        // });

        // setComments({
        //   ...comments,
        //   main: {
        //     data: [...comments.main.data, ...data],
        //     metaData: metaData,
        //   },
        // });
      })
      .catch(error => {
        const message =
          'An internal server error occurred. Please try again later.';
        // setFetchResult({
        //   loading: false,
        //   error: message,
        // });
        // showMessage({
        //   type: 'danger',
        //   message,
        // });
      });
  };

  const handleFetchMoreSubComments = () => {
    if (!showSubComments) {
      setShowSubComments(true);
    } else {
      handleFetchGetSubCommentsByParentCommentId();
    }
  };

  const otherCommentCount = useMemo(() => {
    if (!subComments) return null;

    if (!showSubComments) {
      return subComments.metaData.totalRecords;
    } else {
      if (
        subComments.metaData.currentPage === subComments.metaData.totalPages
      ) {
        return 0;
      }
      return (
        subComments.metaData.totalRecords -
        subComments.metaData.pageSize * subComments.metaData.currentPage
      );
    }
  }, [subComments, showSubComments]);

  useEffect(() => {
    setSubComments(comment.subComments);
  }, []);

  return (
    <View style={styles.container}>
      <View>
        <Image
          style={{height: 36, width: 36, borderRadius: 16}}
          source={{
            uri: comment.user.profilePhotoURL
              ? `https://res.cloudinary.com/sahinmaral/${comment.user.profilePhotoURL}`
              : 'https://res.cloudinary.com/sahinmaral/image/upload/v1699171684/profileImages/default.png',
          }}
        />
      </View>
      <View style={{flex: 1}}>
        <Text style={styles.username.text}>{comment.user.userName}</Text>
        <Text>{comment.message}</Text>
        {subComments &&
          subComments.metaData.totalRecords !== 0 &&
          showSubComments && (
            <FlatList
              data={subComments.data}
              renderItem={renderCommentsDetailItem}
              keyExtractor={item => item.id}
            />
          )}
        {subComments &&
          subComments.metaData.totalRecords !== 0 &&
          otherCommentCount !== 0 && (
            <TouchableOpacity
              onPress={() => handleFetchMoreSubComments(subComments)}>
              <Text
                style={styles.mainCommentShowComments.text}>
                Show other comments ({otherCommentCount})
              </Text>
            </TouchableOpacity>
          )}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {flexDirection: 'row', gap: 10, marginVertical: 10},
  username: {
    text: {fontWeight: 'bold'},
  },
  mainCommentShowComments: {
    text: {color: 'gray'},
  },
  showAllComments: {
    text: {color: 'gray'},
  },
});

export default CommentsDetailItem;
