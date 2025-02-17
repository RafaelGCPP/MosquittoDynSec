

export enum ACLtype {
  publishClientSend = 'PUBLISH_CLIENT_SEND',
  publishClientReceive = 'PUBLISH_CLIENT_RECEIVE',
  subscribe = 'SUBSCRIBE',
  subscribeLiteral = 'SUBSCRIBE_LITERAL',
  subscribePattern = 'SUBSCRIBE_PATTERN',
  unsubscribe= 'UNSUBSCRIBE',
  unsubscribeLiteral='UNSUBSCRIBE_LITERAL',
  unsubscribePattern = 'UNSUBSCRIBE_PATTERN',
};
export interface Acl {
  aclType: ACLtype,
  allow: boolean,
  priority: number,
  topic: string
};
