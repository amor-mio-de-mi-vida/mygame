Generator_PREFIX = (
    '''
    剧情对话AI背景描述（提示词前缀）
    '''
    "Description 1"
    "Description 2"
)


Generator_FORMAT_INSTRUCTIONS = "xxx,TODO"


Generator_SUFFIX = (
    '''
    生成结果格式等要求（提示词后缀）
    '''
    "Description 1"
    "Description 2"
    "Previous conversation history:\n"
    "{chat_history}\n"
    "New input: {input}\n"
)


Bg_Relation_TEMPLATE = (
    '''
    处理对象描述
    '''
    "Description 1"
    "Description 2"
    # 待传入背景信息
    "Background 1: {BG1}"
    "Background 2: {BG2}"
)


Bg_Relation_DESC = (
    # description
    " "
)
