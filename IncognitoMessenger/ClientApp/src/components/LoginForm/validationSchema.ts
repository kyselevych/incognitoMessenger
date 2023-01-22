import * as yup from 'yup';

const messages = {
  required: "required field"
};

export default yup.object({
  login: yup.string().required(messages.required),
  password: yup.string().required(messages.required),
});

