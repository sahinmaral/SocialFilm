import {useEffect, useState} from 'react';

const useCustomForm = formKeys => {
  const [form, setForm] = useState({
    values: null,
    isFocused: null,
  });

  if (!formKeys) {
    throw 'Keys you assigned must be assigned';
  } else if (!Array.isArray(formKeys)) {
    throw 'Keys you assigned must be array';
  }

  useEffect(() => {
    formKeys.forEach(formKey => {
      setForm({
        values: {
          ...form.values,
          [formKey]: '',
        },
        isFocused: {
          ...form.isFocused,
          [formKey]: false,
        },
      });
    });
  }, []);

  const handleFocus = formKey => {
    Object.keys(form.isFocused).forEach(key => {
      if (key === formKey) {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: true},
        });
      } else {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: true},
        });
      }
    });
  };

  const handleBlur = formKey => {
    Object.keys(form.isFocused).forEach(key => {
      if (key === formKey) {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: false},
        });
      } else {
        setForm({
          ...form,
          isFocused: {...form.isFocused, [formKey]: false},
        });
      }
    });
  };

  const handleSetValueOfFormKey = (formKey, value) => {
    setForm({...form, values: {...form.values, [formKey]: value}});
  };

  return {form, handleBlur, handleFocus, handleSetValueOfFormKey};
};

export default useCustomForm;
