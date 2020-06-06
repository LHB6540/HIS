/*
Navicat MariaDB Data Transfer

Source Server         : 47.107.149.229_3306
Source Server Version : 50565
Source Host           : 47.107.149.229:3306
Source Database       : his

Target Server Type    : MariaDB
Target Server Version : 50565
File Encoding         : 65001

Date: 2020-06-06 16:31:14
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for check
-- ----------------------------
DROP TABLE IF EXISTS `check`;
CREATE TABLE `check` (
  `appnum` varchar(255) DEFAULT NULL,
  `cardnum` varchar(255) DEFAULT NULL,
  `regid` varchar(255) DEFAULT NULL,
  `paname` varchar(255) DEFAULT NULL,
  `sex` varchar(255) DEFAULT NULL,
  `age` varchar(255) DEFAULT NULL,
  `class` varchar(255) DEFAULT NULL,
  `zhenduan` varchar(255) DEFAULT NULL,
  `feiyong` varchar(255) DEFAULT NULL,
  `mudi` varchar(255) DEFAULT NULL,
  `riqi` date DEFAULT NULL,
  `docname` varchar(255) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `orders` varchar(255) DEFAULT NULL,
  `items` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of check
-- ----------------------------

-- ----------------------------
-- Table structure for chinesemed
-- ----------------------------
DROP TABLE IF EXISTS `chinesemed`;
CREATE TABLE `chinesemed` (
  `chinesemedid` char(255) NOT NULL,
  `cname` varchar(255) DEFAULT NULL,
  `cguige` varchar(255) DEFAULT NULL,
  `cjiagedanwei` varchar(255) DEFAULT NULL,
  `cdanwei` varchar(255) DEFAULT NULL,
  `ckucun` varchar(255) DEFAULT NULL,
  `czhujiao` varchar(255) DEFAULT NULL,
  `cbeizhu` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`chinesemedid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of chinesemed
-- ----------------------------
INSERT INTO `chinesemed` VALUES ('c01', '茯苓', '规格', '0.3元/克', '克', '1000000', '注脚', '备注');
INSERT INTO `chinesemed` VALUES ('c02', '麻黄汤', '规格', '10元/包', '包', '99', '注脚', '备注');
INSERT INTO `chinesemed` VALUES ('c10', '大枣', '片', '1.5元/克', '克', '184', '无', '无');
INSERT INTO `chinesemed` VALUES ('c11', '甘草', '片', '0.9元/克', '克', '208', '无', '无');
INSERT INTO `chinesemed` VALUES ('c12', '鹿茸', '段', '0.9元/克', '克', '764', '无', '无');
INSERT INTO `chinesemed` VALUES ('c13', '紫河车', '段', '1.1元/克', '克', '764', '无', '无');
INSERT INTO `chinesemed` VALUES ('c14', '冬虫夏草', '段', '1.1元/克', '克', '737', '无', '无');
INSERT INTO `chinesemed` VALUES ('c15', '蛇床子', '片', '1.6元/克', '克', '381', '无', '无');
INSERT INTO `chinesemed` VALUES ('c16', '淫羊藿', '片', '0.5元/克', '克', '533', '无', '无');
INSERT INTO `chinesemed` VALUES ('c17', '巴戟天', '片', '1.7元/克', '克', '665', '无', '无');
INSERT INTO `chinesemed` VALUES ('c18', '仙茅', '片', '1.8元/克', '克', '205', '无', '无');
INSERT INTO `chinesemed` VALUES ('c19', '肉苁蓉', '段', '0.7元/克', '克', '994', '无', '无');
INSERT INTO `chinesemed` VALUES ('c20', '锁阳', '段', '1.6元/克', '克', '461', '无', '无');
INSERT INTO `chinesemed` VALUES ('c21', '胡桃仁', '段', '1.7元/克', '克', '561', '无', '无');
INSERT INTO `chinesemed` VALUES ('c22', '熟地', '片', '1.9元/克', '克', '940', '无', '无');
INSERT INTO `chinesemed` VALUES ('c23', '首乌', '片', '1.5元/克', '克', '190', '无', '无');
INSERT INTO `chinesemed` VALUES ('c24', '当归', '片', '0.7元/克', '克', '177', '无', '无');
INSERT INTO `chinesemed` VALUES ('c25', '白芍', '片', '0.5元/克', '克', '706', '无', '无');
INSERT INTO `chinesemed` VALUES ('c26', '阿胶', '片', '0.5元/克', '克', '910', '无', '无');
INSERT INTO `chinesemed` VALUES ('c27', '桑椹子', '段', '1.7元/克', '克', '959', '无', '无');
INSERT INTO `chinesemed` VALUES ('c28', '龙眼肉', '块', '1.6元/克', '克', '761', '无', '无');
INSERT INTO `chinesemed` VALUES ('c29', '鸡血藤', '段', '1.5元/克', '克', '896', '无', '无');
INSERT INTO `chinesemed` VALUES ('c30', '石斛', '片', '1.3元/克', '克', '993', '无', '无');
INSERT INTO `chinesemed` VALUES ('c31', '玉竹', '片', '1.1元/克', '克', '359', '无', '无');
INSERT INTO `chinesemed` VALUES ('c32', '百合', '片', '2元/克', '克', '549', '无', '无');
INSERT INTO `chinesemed` VALUES ('c33', '枸杞子', '块', '0.5元/克', '克', '758', '无', '无');
INSERT INTO `chinesemed` VALUES ('c34', '侦子', '片', '2元/克', '克', '813', '无', '无');

-- ----------------------------
-- Table structure for chufang
-- ----------------------------
DROP TABLE IF EXISTS `chufang`;
CREATE TABLE `chufang` (
  `regid` char(255) DEFAULT NULL,
  `paid` int(11) DEFAULT NULL,
  `painame` varchar(255) DEFAULT NULL,
  `medid` char(255) DEFAULT NULL,
  `medname` varchar(255) DEFAULT NULL,
  `medjiage` varchar(255) DEFAULT NULL,
  `meddanwei` varchar(255) DEFAULT NULL,
  `medshuliang` varchar(255) DEFAULT NULL,
  `medzongjia` varchar(255) DEFAULT NULL,
  `medyongfa` varchar(255) DEFAULT NULL,
  `medyongliang` varchar(255) DEFAULT NULL,
  `medkaifang` varchar(255) DEFAULT NULL,
  `meddate` date DEFAULT NULL,
  `medclass` varchar(255) DEFAULT NULL,
  `meddoctor` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of chufang
-- ----------------------------

-- ----------------------------
-- Table structure for classanddoctor
-- ----------------------------
DROP TABLE IF EXISTS `classanddoctor`;
CREATE TABLE `classanddoctor` (
  `ID` char(50) NOT NULL,
  `name` char(50) DEFAULT NULL,
  `classType` varchar(255) DEFAULT NULL,
  `class` varchar(255) DEFAULT NULL,
  `level` int(8) DEFAULT NULL,
  `phone` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of classanddoctor
-- ----------------------------
INSERT INTO `classanddoctor` VALUES ('0201', '王医生', '骨科', '骨伤科', '3', '123456');
INSERT INTO `classanddoctor` VALUES ('0202', '张医生', '外科', '运动康复课', '2', '121314');
INSERT INTO `classanddoctor` VALUES ('11', 'Name11', 'classType2', 'class21', '3', '121314');
INSERT INTO `classanddoctor` VALUES ('12', 'Name12', 'classType2', 'class21', '4', '121314');
INSERT INTO `classanddoctor` VALUES ('13', 'Name13', 'classType2', 'class21', '1', '121314');
INSERT INTO `classanddoctor` VALUES ('14', 'Name14', 'classType2', 'class22', '2', '121314');
INSERT INTO `classanddoctor` VALUES ('15', 'Name15', 'classType2', 'class22', '3', '121314');
INSERT INTO `classanddoctor` VALUES ('16', 'Name16', 'classType2', 'class22', '4', '121314');
INSERT INTO `classanddoctor` VALUES ('17', 'Name17', 'classType2', 'class23', '1', '121314');
INSERT INTO `classanddoctor` VALUES ('18', 'Name18', 'classType3', 'class32', '2', '121314');
INSERT INTO `classanddoctor` VALUES ('19', 'Name19', 'classType3', 'class32', '3', '121314');
INSERT INTO `classanddoctor` VALUES ('2', 'Name2', 'ClassType1', 'class11', '3', '121314');
INSERT INTO `classanddoctor` VALUES ('20', 'Name20', 'classType3', 'class33', '4', '121314');
INSERT INTO `classanddoctor` VALUES ('21', 'Name21', 'classType3', 'class33', '1', '121314');
INSERT INTO `classanddoctor` VALUES ('22', 'Name22', 'classType3', 'class33', '2', '121314');
INSERT INTO `classanddoctor` VALUES ('23', 'Name23', 'classType3', 'class33', '3', '121314');
INSERT INTO `classanddoctor` VALUES ('24', 'Name24', 'classType3', 'class31', '4', '121314');
INSERT INTO `classanddoctor` VALUES ('25', 'Name25', 'classType3', 'class31', '1', '121314');
INSERT INTO `classanddoctor` VALUES ('3', 'Name3', 'ClassType1', 'class11', '4', '121314');
INSERT INTO `classanddoctor` VALUES ('4', 'Name4', 'ClassType1', 'class11', '1', '121314');
INSERT INTO `classanddoctor` VALUES ('5', 'Name5', 'ClassType1', 'class12', '2', '121314');
INSERT INTO `classanddoctor` VALUES ('6', 'Name6', 'ClassType1', 'class13', '4', '121314');
INSERT INTO `classanddoctor` VALUES ('7', 'Name7', 'ClassType1', 'class14', '3', '121314');
INSERT INTO `classanddoctor` VALUES ('8', 'Name8', 'ClassType1', 'class13', '1', '121314');
INSERT INTO `classanddoctor` VALUES ('9', 'Name9', 'ClassType1', 'class13', '2', '121314');

-- ----------------------------
-- Table structure for patient
-- ----------------------------
DROP TABLE IF EXISTS `patient`;
CREATE TABLE `patient` (
  `CardNum` char(50) CHARACTER SET utf8 NOT NULL,
  `paName` char(50) CHARACTER SET utf8 DEFAULT NULL,
  `paSex` int(11) DEFAULT NULL,
  `paBorth` date DEFAULT NULL,
  `paIDType` int(11) DEFAULT NULL,
  `paID` char(50) CHARACTER SET utf8 DEFAULT NULL,
  `paAge` char(8) CHARACTER SET utf8 DEFAULT NULL,
  `paPhone` char(100) CHARACTER SET utf8 DEFAULT NULL,
  `paAddress` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `paAllergy` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`CardNum`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of patient
-- ----------------------------
INSERT INTO `patient` VALUES ('1', '张沈静', '1', '2020-04-15', '2', '4.49829202005098E17', '17岁', '1111', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('10', '王安翔', '1', '2019-05-09', '0', '4.49829202005098E17', '20岁', '13827465936', '广东省广州市番禺区瓜沥路爱大家花园3栋', '青霉素');
INSERT INTO `patient` VALUES ('11', '李牧歌', '1', '1995-02-09', '0', '4.41256199502053E16', '25岁', '13022056482', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('12', '张高杰', '1', '1995-02-09', '0', '4.41256199502053E16', '25岁', '13022056482', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('13', '李凡白', '1', '1998-01-01', '0', '4.49829202005098E17', '19岁', '13382759640', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('14', '洪梓琬', '2', '1999-02-02', '0', '4.41256199502053E16', '30岁', '15928374608', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('15', '刘明诚', '1', '1953-05-04', '0', '4.40198195305047E17', '20岁', '13827465936', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('16', '刘洁玉', '2', '1967-06-14', '0', '4.49829202005098E17', '32岁', '13774857699', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('17', '洪云逸', '1', '1978-07-08', '0', '4.40198195305047E17', '50岁', '13382759640', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('18', '洪鹏涛', '1', '2000-06-27', '0', '4.41256199502053E16', '19岁', '15928374608', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('19', '王昌勋', '1', '2001-04-28', '0', '4.49829202005098E17', '20岁', '13827465936', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('2', '张乐水', '2', '2020-04-29', '0', '4.41256199502053E16', '20岁', '15928374608', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('20', '李鹏天', '1', '2002-09-03', '0', '4.40198195305047E17', '40岁', '13774857699', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('202005161030132890', '王丽娜', '2', '1999-02-16', '0', '440827199902162890', '21', '13672288987', '广东省广州市海珠区富贵路王者如云花园3栋', '板蓝根');
INSERT INTO `patient` VALUES ('202005161539122354', '李三', '2', '2020-05-16', '0', '', '', '', '广州白云', '阿司匹林');
INSERT INTO `patient` VALUES ('202005161749231111', '小红', '1', '2020-05-16', '0', '111111', '12', '1111111', '广州', '青霉素');
INSERT INTO `patient` VALUES ('202005161752421212', '小红', '1', '2020-05-16', '0', '1121112111211121212', '11', '110', '广州跟着跟着该责怪找个工作跟着跟着', '青霉素');
INSERT INTO `patient` VALUES ('202005161757012112', '夏老师', '1', '2020-05-16', '0', '112112', '112', '112', '112112', '112112');
INSERT INTO `patient` VALUES ('202005201627042525', '测试1', '2', '2020-05-20', '0', '13242525', '', '', '', '');
INSERT INTO `patient` VALUES ('202005210919331111', 'test', '2', '2020-05-21', '0', '111111', '', '', '', '');
INSERT INTO `patient` VALUES ('21', '张含巧', '1', '1979-04-03', '0', '4.40198195305047E17', '42岁', '13382759640', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('22', '李良骏', '1', '2003-07-03', '0', '4.41256199502053E16', '19岁', '13382759640', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('23', '李天韵', '1', '1989-07-02', '0', '4.49829202005098E17', '37岁', '15928374608', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('24', '夏安珊', '2', '2000-08-02', '0', '4.40198195305047E17', '20岁', '13827465936', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('25', '李冰蓝', '2', '1999-06-07', '0', '4.49829202005098E17', '19岁', '13382759640', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('26', '刘雁卉', '2', '2000-05-06', '0', '4.41256199502053E16', '17岁', '13827465936', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('27', '王心香', '2', '2000-07-08', '0', '4.40198195305047E17', '20岁', '13774857699', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('28', '李秋珊', '2', '2000-05-08', '0', '4.49829202005098E17', '25岁', '13382759640', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('29', '夏芷若', '2', '2003-06-01', '0', '4.41256199502053E16', '19岁', '15928374608', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('3', '夏雅素', '2', '2020-04-23', '0', '4.49829202005098E17', '19岁', '13382759640', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('30', '李文心', '2', '1999-07-06', '0', '4.49829202005098E17', '21岁', '13382759640', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('4', '刘忆柏', '1', '2020-04-23', '0', '4.41256199502053E16', '19岁', '13827465936', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('5', '刘俨雅', '2', '2020-04-23', '0', '4.41256199502053E16', '20岁', '15928374608', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('6', '洪秋蝶', '2', '2020-05-09', '0', '4.49829202005098E17', '20岁', '13382759640', '广东省广州市xx区xx路xx号', '异种血清');
INSERT INTO `patient` VALUES ('7', '王暄美', '2', '2020-05-09', '0', '4.41256199502053E16', '20岁', '13827465936', '广东省广州市xx区xx路xx号', '青霉素');
INSERT INTO `patient` VALUES ('8', '王听安', '2', '2020-05-09', '0', '4.41256199502053E16', '20岁', '13382759640', '广东省广州市xx区xx路xx号', '链霉素');
INSERT INTO `patient` VALUES ('9', '李怀玉', '2', '2020-05-09', '0', '132435354343', '20岁', '15928374608', '广东省广州市xx区xx路xx号', '异种血清');

-- ----------------------------
-- Table structure for regandduty
-- ----------------------------
DROP TABLE IF EXISTS `regandduty`;
CREATE TABLE `regandduty` (
  `regID` int(11) NOT NULL,
  `doctorID` char(255) DEFAULT NULL,
  `data` date DEFAULT NULL,
  `paiID` char(255) DEFAULT NULL,
  `time` time DEFAULT NULL,
  `ifUse` int(11) DEFAULT NULL,
  `treatstatus` int(11) DEFAULT NULL,
  `regpay` decimal(10,0) DEFAULT NULL,
  `level` varchar(255) DEFAULT NULL,
  `regtype` varchar(255) DEFAULT NULL,
  `paytype` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`regID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of regandduty
-- ----------------------------
INSERT INTO `regandduty` VALUES ('21', '0201', '2020-05-22', '', '08:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('22', '0201', '2020-05-22', '', '08:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('23', '0201', '2020-05-22', '', '08:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('24', '0201', '2020-05-22', '', '08:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('25', '0201', '2020-05-22', '', '09:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('26', '0201', '2020-05-22', '', '09:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('27', '0201', '2020-05-22', '', '09:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('28', '0201', '2020-05-22', '', '09:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('29', '0201', '2020-05-22', '', '10:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('30', '0202', '2020-05-22', '1', '08:00:00', '1', '0', '20', '专家', '门诊', '支付宝');
INSERT INTO `regandduty` VALUES ('31', '0201', '0000-00-00', '', '08:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('32', '0201', '0000-00-00', '', '08:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('33', '0201', '0000-00-00', '', '08:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('34', '0201', '0000-00-00', '', '08:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('35', '0201', '0000-00-00', '', '09:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('36', '0201', '0000-00-00', '', '09:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('37', '0201', '0000-00-00', '', '09:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('38', '0201', '0000-00-00', '', '09:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('39', '0201', '0000-00-00', '', '10:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('40', '0202', '0000-00-00', '1', '08:00:00', '1', '0', '20', '专家', '门诊', '支付宝');
INSERT INTO `regandduty` VALUES ('41', '0201', '0000-00-00', '', '08:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('42', '0201', '0000-00-00', '', '08:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('43', '0201', '0000-00-00', '', '08:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('44', '0201', '0000-00-00', '', '08:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('45', '0201', '0000-00-00', '', '09:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('46', '0201', '0000-00-00', '', '09:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('47', '0201', '0000-00-00', '', '09:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('48', '0201', '0000-00-00', '', '09:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('49', '0201', '0000-00-00', '', '10:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('50', '0202', '0000-00-00', '1', '08:00:00', '1', '0', '20', '专家', '门诊', '支付宝');
INSERT INTO `regandduty` VALUES ('51', '0201', '0000-00-00', '', '08:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('52', '0201', '0000-00-00', '', '08:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('53', '0201', '0000-00-00', '', '08:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('54', '0201', '0000-00-00', '', '08:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('55', '0201', '0000-00-00', '', '09:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('56', '0201', '0000-00-00', '', '09:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('57', '0201', '0000-00-00', '', '09:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('58', '0201', '0000-00-00', '', '09:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('59', '0201', '0000-00-00', '', '10:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('60', '0202', '0000-00-00', '1', '08:00:00', '1', '0', '20', '专家', '门诊', '支付宝');
INSERT INTO `regandduty` VALUES ('61', '0201', '0000-00-00', '', '08:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('62', '0201', '0000-00-00', '', '08:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('63', '0201', '0000-00-00', '', '08:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('64', '0201', '0000-00-00', '', '08:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('65', '0201', '0000-00-00', '', '09:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('66', '0201', '0000-00-00', '', '09:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('67', '0201', '0000-00-00', '', '09:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('68', '0201', '0000-00-00', '', '09:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('69', '0201', '0000-00-00', '', '10:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('70', '0202', '0000-00-00', '1', '08:00:00', '1', '0', '20', '专家', '门诊', '支付宝');
INSERT INTO `regandduty` VALUES ('71', '0201', '0000-00-00', '', '08:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('72', '0201', '0000-00-00', '', '08:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('73', '0201', '0000-00-00', '', '08:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('74', '0201', '0000-00-00', '', '08:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('75', '0201', '0000-00-00', '', '09:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('76', '0201', '0000-00-00', '', '09:15:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('77', '0201', '0000-00-00', '', '09:30:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('78', '0201', '0000-00-00', '', '09:45:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('79', '0201', '0000-00-00', '', '10:00:00', '0', '0', null, '', '', '');
INSERT INTO `regandduty` VALUES ('80', '0202', '0000-00-00', '1', '08:00:00', '1', '0', '20', '专家', '门诊', '支付宝');
INSERT INTO `regandduty` VALUES ('202012020', '0201', '2020-05-20', '2', '20:00:00', '1', '0', '20', '专家', '门诊', '现金');
INSERT INTO `regandduty` VALUES ('202022020', '0202', '2020-05-21', '2', '20:00:00', '1', '0', '30', '主任医师', '门诊', '支付宝');

-- ----------------------------
-- Table structure for regrec
-- ----------------------------
DROP TABLE IF EXISTS `regrec`;
CREATE TABLE `regrec` (
  `regid` char(255) NOT NULL,
  `doctorid` char(255) DEFAULT NULL,
  `doctorname` varchar(255) DEFAULT NULL,
  `paiid` char(255) DEFAULT NULL,
  `painame` varchar(255) DEFAULT NULL,
  `guomin` varchar(255) DEFAULT NULL,
  `history` varchar(255) DEFAULT NULL,
  `bodyhea` varchar(255) DEFAULT NULL,
  `talk` varchar(255) DEFAULT NULL,
  `now` varchar(255) DEFAULT NULL,
  `firstres` varchar(255) DEFAULT NULL,
  `withres` varchar(255) DEFAULT NULL,
  `class` varchar(255) DEFAULT NULL,
  `yizhu` varchar(255) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `chufang` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`regid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of regrec
-- ----------------------------

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `name` varchar(50) CHARACTER SET utf8 NOT NULL,
  `passwd` varchar(100) CHARACTER SET utf8 NOT NULL,
  `type` int(11) DEFAULT NULL,
  `sex` char(255) CHARACTER SET utf8 DEFAULT NULL,
  `date` date DEFAULT NULL,
  `phone` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  `realname` varchar(255) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of user
-- ----------------------------

-- ----------------------------
-- Table structure for westmed
-- ----------------------------
DROP TABLE IF EXISTS `westmed`;
CREATE TABLE `westmed` (
  `westmedid` char(255) NOT NULL,
  `wname` varchar(255) DEFAULT NULL,
  `wguige` varchar(255) DEFAULT NULL,
  `wyaojia` varchar(255) DEFAULT NULL,
  `wdanwei` varchar(255) DEFAULT NULL,
  `wyaowugongxiao` varchar(255) DEFAULT NULL,
  `wyaowuzhuzhi` varchar(255) DEFAULT NULL,
  `wshiyongshuoming` varchar(255) DEFAULT NULL,
  `wkucun` double(255,0) DEFAULT NULL,
  `wbeizhu` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`westmedid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of westmed
-- ----------------------------
INSERT INTO `westmed` VALUES ('w01', '阿莫西林胶囊', '0.25g*24粒/盒', '10元/盒', '盒', '治疗敏感菌感染', '敏感菌感', '内服', '399', '无');
INSERT INTO `westmed` VALUES ('w02', '头孢克洛缓释片', '0.375g*4片/盒', '34.9元/盒', '盒', '治疗敏感菌感染', '敏感菌感', '内服', '400', '无');
INSERT INTO `westmed` VALUES ('w03', '头孢克肟干混悬剂', '50mg*6袋/盒', '5元/盒', '盒', '治疗头孢克肟敏感的链球菌属（肠球菌除外），肺炎球菌、淋球菌、卡他布兰汉球菌、大肠杆菌、克雷伯杆菌属、沙雷菌属、变形杆菌属及流感杆菌等引起的细菌感染性疾病', '支气管炎、支气管扩张症；肾盂肾炎、膀胱炎、淋球菌性尿道炎；胆囊炎、胆管炎；猩红热；中耳炎、副鼻窦炎。', '内服', '200', '无');
INSERT INTO `westmed` VALUES ('w04', '阿司匹林泡腾片', '0.5g*10片/盒', '5元/盒', '盒', '解热镇痛', '普通感冒或流行性感冒引起的发热，也用于缓解轻至中度疼痛如头痛、关节痛、偏头痛、牙痛、肌肉痛、神经痛、痛经', '内服', '500', '无');
INSERT INTO `westmed` VALUES ('w05', '小儿氨酚黄那敏颗粒', '5g*10袋/盒', '11元/盒', '盒', '缓解儿童普通感冒及流行性感冒引起的发热、头痛、四肢酸痛、打喷嚏、流鼻涕、鼻塞、咽痛等症状。', '感冒', '内服', '300', '无');
INSERT INTO `westmed` VALUES ('w06', '氯雷他定片', '10mg*6片/盒', '22元/盒', '盒', '缓解过敏性鼻炎有关的症状,如喷嚏、流涕、鼻痒、鼻塞以及眼部痒及烧灼感。', '过敏性鼻炎', '内服', '200', '无');
INSERT INTO `westmed` VALUES ('w07', '盐酸曲普利啶胶囊', '2.5mg*20粒/盒', '12元/盒', '盒', '治疗各种过敏性疾患', '过敏性鼻炎、荨麻疹、过敏性结膜炎、皮肤瘙痒症等。', '内服', '280', '无');
INSERT INTO `westmed` VALUES ('w08', '奥利司他胶囊', '0.12g*7粒/盒', '50.60元/盒', '盒', '肥胖或体重超重患者（体重指数≥24）的治疗。', '肥胖症', '内服', '100', '无');
INSERT INTO `westmed` VALUES ('w09', '爱赛平盐酸氮卓斯汀鼻喷剂', '10ml*1支/盒', '53.80元*1支/盒', '盒', '治疗鼻炎', '季节性过敏性鼻炎(花粉症)、常年性过敏性鼻炎', '外用', '100', '无');
INSERT INTO `westmed` VALUES ('w10', '复方硫酸新霉素滴眼液', '6ml*1支/盒', '10元/盒', '盒', '治疗各类炎症', '急﹑慢性结膜炎﹑角膜炎﹑巩膜炎﹑葡萄膜炎﹑急性虹膜炎,以及白内障﹑青光眼及角膜移植术后﹑眼部机械或化学烧伤处理。', '外用', '100', '无');
INSERT INTO `westmed` VALUES ('w11', '氧氟沙星滴耳液', '5ml*1支/盒', '9元/盒', '盒', '治疗敏感菌引起的中耳炎、外耳道炎、鼓膜炎', '中耳炎、外耳道炎、鼓膜炎', '外用', '100', '无');
INSERT INTO `westmed` VALUES ('w12', '萘敏维滴眼液', '10ml*1支/盒', '16元/盒', '盒', '缓解眼睛疲劳、结膜充血以及眼睛发痒等症状', '眼睛疲劳、结膜充血以及眼睛发痒', '外用', '100', '无');
INSERT INTO `westmed` VALUES ('w13', '曲咪新乳膏', '10g*1支/盒', '2元/盒', '盒', '用于湿疹、接触性皮炎、脂溢性皮炎、神经性皮炎、体癣、股癣以及手足癣等。', '湿疹、接触性皮炎、脂溢性皮炎、神经性皮炎、体癣、股癣以及手足癣', '外用', '300', '无');
INSERT INTO `westmed` VALUES ('w14', '克罗米通乳膏', '10g*1支/盒', '2.80元/盒', '盒', '治疗疥疮及皮肤瘙痒', '疥疮及皮肤瘙痒', '外用', '250', '无');
INSERT INTO `westmed` VALUES ('w15', '他克莫司胶囊', '1mg*50粒/盒', '640.60元/盒', '盒', '预防肝脏或肾脏移植术后的移植物排斥反应', '肝脏或肾脏移植术后的移植物排斥反应', '内服', '200', '无');
INSERT INTO `westmed` VALUES ('w16', '聚甲酚磺醛溶液', '10ml/瓶', '22元/瓶', '瓶', '治疗宫颈糜烂、宫颈炎、各类阴道感染（如细菌、滴虫和霉菌引起的白带增多）、外阴瘙痒、及使用子宫托造成的压迫性溃疡、宫颈息肉切除或切片检查后的止血,尖锐湿疣及加速电凝治疗后的伤口愈合；还可用于乳腺炎的预防（乳头皲裂的烧灼）', '宫颈糜烂、宫颈炎、各类阴道感染', '外用', '100', '无');
INSERT INTO `westmed` VALUES ('w17', '缩宫素注射液', '1ml*1支/盒', '13.9元/盒', '盒', '用于引产、催产、产后及流产后因宫缩无力或缩复不良而引起的子宫出血；了解胎盘储备功能(催产素激惹试验)。', '引产、催产、产后及流产后因宫缩无力或缩复不良而引起的子宫出血', '注射', '500', '无');
INSERT INTO `westmed` VALUES ('w18', '维D钙咀嚼片', '120片*1支/盒', '79元/盒', '盒', '用于妊娠和哺乳期妇女、更年期妇女、老年人、儿童等的钙补充剂，并帮助防治骨质疏松症。', '缺钙、骨质疏松症', '内服', '595', '无');
INSERT INTO `westmed` VALUES ('w19', '门冬胰岛素注射液', '3ml*1支/盒', '179元/盒', '盒', '用于治疗糖尿病', '糖尿病', '注射', '200', '无');
INSERT INTO `westmed` VALUES ('w20', '依巴斯汀片', '10mg*10片/盒', '37元/盒', '盒', '适用于伴有或不伴有过敏性结膜炎的过敏性鼻炎(季节性和常年性)、慢性特发性荨麻疹的对症治疗。', '过敏性鼻炎(季节性和常年性)', '内服', '100', '无');
INSERT INTO `westmed` VALUES ('w21', '白葡奈氏菌片', '0.3mg*40片/盒', '74元/盒', '盒', '用于慢性气管炎及喘息性气管炎', '慢性气管炎及喘息性气管炎', '内服', '350', '无');
INSERT INTO `westmed` VALUES ('w22', '蒙脱石散', '3g*10袋/盒', '19元/盒', '盒', '用于成人及儿童急、慢性腹泻', '急、慢性腹泻', '内服', '148', '无');
INSERT INTO `westmed` VALUES ('w23', '盐酸克林霉素葡萄糖注射液', '200ml/瓶', '34元/瓶', '瓶', '适用于革兰阳性菌引起的各种感染性疾病、厌氧菌引起的各种感染性疾病', '扁桃体炎﹑化脓性中耳炎﹑鼻窦炎；急性支气管炎﹑慢性支气管炎急性发作﹑肺炎﹑肺脓肿和支气管扩张合并感染；脓胸﹑肺脓肿﹑厌氧菌性肺病', '注射', '256', '无');
INSERT INTO `westmed` VALUES ('w24', '氧氟沙星片', '0.1g*12片/盒', '2.3元/盒', '盒', '适用于敏感菌引起的各种疾病', '泌尿生殖系统感染，包括单纯性、复杂性尿路感染、细菌性前列腺炎、淋病奈瑟菌尿道炎或宫颈炎(包括产酶株所致者)；呼吸道感染，包括敏感革兰阴性杆菌所致支气管感染急性发作及肺部感染；胃肠道感染，由志贺菌属、沙门菌属、产肠毒素大肠杆菌、亲水气单胞菌、副溶血弧菌等所致。', '内服', '200', '无');
INSERT INTO `westmed` VALUES ('w25', '999感冒灵颗粒', '10g*9袋/盒', '9.5元/盒', '盒', '解热镇痛', '感冒引起的头痛，发热，鼻塞，流涕，咽痛。', '内服', '200', '无');
INSERT INTO `westmed` VALUES ('w26', '小儿氨酚黄那敏颗粒', '5g*10袋/盒', '11元/盒', '盒', '缓解儿童普通感冒及流行性感冒引起的发热、头痛、四肢酸痛、打喷嚏、流鼻涕、鼻塞、咽痛等症状。', '感冒', '内服', '600', '无');
INSERT INTO `westmed` VALUES ('w27', '美斯钠注射液', '4ml*15支/盒', '78元/盒', '盒', '预防oxazaphosphrine类药物(包括异环磷酰胺、环磷酰胺、trophasfamide)引起的泌尿道毒性。', '泌尿道毒性', '注射', '500', '无');
INSERT INTO `westmed` VALUES ('w28', '格列喹酮片', '30mg*60片/盒', '53元/盒', '盒', '用于治疗2型糖尿病', '2型糖尿病', '内服', '100', '无');
INSERT INTO `westmed` VALUES ('w29', '氯化钾缓释片', '0.5mg*16片/盒', '18元/盒', '盒', '治疗低钾血症:各种原因引起的低钾血症，如进食不足﹑呕吐﹑严重腹泻﹑应用排钾利尿药﹑低钾性家族周期性麻痹﹑长期应用糖皮质激素和补充高渗葡萄糖', '低钾血症', '内服', '200', '无');
INSERT INTO `westmed` VALUES ('w30', '诺氟沙星滴眼液', '8ml*1支/盒', '3元/盒', '盒', '用于敏感菌所致的外眼感染，如结膜炎﹑角膜炎﹑角膜溃疡等', '结膜炎﹑角膜炎﹑角膜溃疡', '外用', '100', '无');
INSERT INTO `westmed` VALUES ('w31', '结合雌激素片', '0.3mg*1片/盒', '44元/盒', '盒', '治疗中-重度与绝经相关的血管舒缩症状；治疗外阴和阴道萎缩', '血管舒缩症状', '内服', '200', '无');
INSERT INTO `westmed` VALUES ('w32', '氨甲苯酸注射液', '10ml*5支/盒', '5.7元/盒', '盒', '用于因原发性纤维蛋白溶解过度所引起的出血，包括急性和慢性、局限性或全身性的高纤溶出血，后者常见于癌肿、白血病、妇产科意外、严重肝病出血等。', '高纤溶出血', '注射', '300', '无');
INSERT INTO `westmed` VALUES ('w33', '康王酮康唑洗剂', '50ml*1支/盒', '23元/盒', '盒', '用于头皮糠疹（头皮屑）、局部性花斑癣、脂溢性皮炎。', '头皮糠疹（头皮屑）、局部性花斑癣、脂溢性皮炎', '外用', '250', '无');
