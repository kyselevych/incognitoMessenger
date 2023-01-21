import * as yup from 'yup';

const messages = {
  required: "required field"
};

export default yup.object({
  login: yup.string().required(messages.required).min(6),
  password: yup.string().required(messages.required).min(8),
  repeatPassword: yup.string().required(messages.required).oneOf([yup.ref('password'), null], 'passwords must match'),
  nickname: yup.string().required(messages.required).min(6)
});

