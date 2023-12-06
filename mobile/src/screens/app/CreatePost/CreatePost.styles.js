import {StyleSheet} from 'react-native';

const styles = StyleSheet.create({
  container: {padding: 10, flex: 1, gap: 10},
  content: {
    container: {flex: 1 / 10},
    input: {backgroundColor: 'white', padding: 5},
  },
  images: {
    container: {flex: 4 / 10, gap: 10},
    imageUpload: {
      container: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
      },
      addRow: {
        borderWidth: 1,
        borderColor: "black",
        padding: 5,
        alignSelf: 'flex-start',
      },
    },
  },
  filmToPost: {
    container: {flex: 4 / 10, gap: 10},
    input: {borderWidth: 1, borderColor: "black"},
    dropdown: {
      row: {
        container: {
          borderWidth: 1,
          borderColor: 'lightgray',
          padding: 5,
          flexDirection: 'row',
          justifyContent: 'space-between',
        },
      },
    },
  },
  submitButton: {
    container: {flex: 1 / 10, justifyContent: 'center'},
  },
});

export default styles;
