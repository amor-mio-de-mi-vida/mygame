from langchain.agents import initialize_agent
from langchain.llms import OpenAI
from langchain.memory import ConversationBufferWindowMemory

import prompts
from tools import get_tools


class Generator:
    def __init__(self, verbose=False, max_memory=20):
        # llm = OpenAI(
        #     temperature=0.2,
        #     model_name="gpt-3.5-turbo",
        # )
        llm = OpenAI(model_name="text-davinci-003", max_tokens=1024)
        # 连续对话上文滑动窗口
        memory = ConversationBufferWindowMemory(
            memory_key="chat_history",
            output_key="output",
            input_key="input",
            k=max_memory,
        )

        tools = get_tools(llm)

        self.agent = initialize_agent(
            tools, llm,
            agent="conversational-react-description",
            verbose=verbose,
            memory=memory,
            return_intermediate_steps=True,
            agent_kwargs={
                'prefix': prompts.Generator_PREFIX,
                'format_instructions': prompts.Generator_FORMAT_INSTRUCTIONS,
                'suffix': prompts.Generator_SUFFIX,
                'input_variables': ["input", "chat_history"],
            },
        )

    def __call__(self, input_str):
        result = self.agent({"input": input_str, "path_list": None})
        return result["output"]
